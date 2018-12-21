using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Interfaces;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;

namespace s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEventHandlers
{
    public class RegistroDePortariaSendoCriadoHandlerParaCertificacoes : IHandler<RegistroDePortariaSendoCriadoEvent>
    {
        private readonly IFazendaCertificacaoMovimentacaoRepository _fazendaCertificacaoMovimentacaoRepository;

        public RegistroDePortariaSendoCriadoHandlerParaCertificacoes(
            IFazendaCertificacaoMovimentacaoRepository fazendaCertificacaoMovimentacaoRepository)
        {
            _fazendaCertificacaoMovimentacaoRepository = fazendaCertificacaoMovimentacaoRepository;
        }

        public void Handle(RegistroDePortariaSendoCriadoEvent args)
        {
            foreach (var notaFiscalGraos in args.RegistroDePortaria.NotasFiscais)
            {
                if (notaFiscalGraos.PossuiCertificados()) continue;

                var fazendacertificacoesMovimentacao = _fazendaCertificacaoMovimentacaoRepository.GetCertificacoesEmVigencia(
                    notaFiscalGraos.DestinatarioFazendaId,
                    notaFiscalGraos.TipoGrao,
                    notaFiscalGraos.NotaFiscalDataEmissao);

                foreach (var fazendacertificacaoMovimentacao in fazendacertificacoesMovimentacao)
                {
                    notaFiscalGraos.AdicionaCertificado(new NotaFiscalGraosCertificado()
                        {
                            CertificadoEmissorId = fazendacertificacaoMovimentacao.CertificacaoEmissor.Id,
                            CertificadoEmissorCodigo = fazendacertificacaoMovimentacao.CertificacaoEmissor.Codigo,
                            CertificadoEmissorNome = fazendacertificacaoMovimentacao.CertificacaoEmissor.Nome,
                            CertificacaoNumero = fazendacertificacaoMovimentacao.CertificacaoNumero
                        }
                    );
                }
            }
        }
    }
}