using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Balanca.Interfaces;
using s4t.Erp.Graos.Domain.Core.CommandHandler;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Inputs;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Results;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Commands.Handlers
{
    public class DocumentoEntradaCommandHandler : CommandHandler,
        ICommandHandler<AbreDocumentoEntradaCommand>,
        ICommandHandler<ComplementaDocumentoEntradaComLotesEPesosCommand>,
        ICommandHandler<ComplementaDocumentoEntradaComPesosCommand>
    {
        private readonly IFilialRepository _filialRepository;
        private readonly INotaFiscalGraosRepository _notaFiscalGraosRepository;
        private readonly IDocumentoEntradaRepository _documentoEntradaRepository;
        private readonly IEmbalagemRepository _embalagemRepository;
        private readonly ITicketPesagemMovimentacaoRepository _ticketPesagemMovimentacaoRepository;

        public DocumentoEntradaCommandHandler(
            IFilialRepository filialRepository,
            INotaFiscalGraosRepository notaFiscalGraosRepository,
            IDocumentoEntradaRepository documentoEntradaRepository,
            IEmbalagemRepository embalagemRepository,
            ITicketPesagemMovimentacaoRepository ticketPesagemMovimentacaoRepository,
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _filialRepository = filialRepository;
            _notaFiscalGraosRepository = notaFiscalGraosRepository;
            _documentoEntradaRepository = documentoEntradaRepository;
            _embalagemRepository = embalagemRepository;
            _ticketPesagemMovimentacaoRepository = ticketPesagemMovimentacaoRepository;
        }

        public ICommandResult Handle(AbreDocumentoEntradaCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiTipoOperacaoValido()))
            {
                return new AbreDocumentoEntradaCommandResult(Guid.Empty, "");
            }

            //Validações de coisas que vão em repositório
            var filial = _filialRepository.ObterPorId(command.FilialId);

            var notaFiscalGraos = _notaFiscalGraosRepository.GetById(command.NotaFiscalGraosId);

            if (!(command.PossuiFilialInformada(filial)
                  & command.PossuiNotaFiscalGraosInformada(notaFiscalGraos)
                  && command.PossuiNotaFiscalGraosDesvinculada(notaFiscalGraos)))
            {
                return new AbreDocumentoEntradaCommandResult(Guid.Empty, "");
            }

            var proximoNumeroDocumentoEntrada = _documentoEntradaRepository.ObterProximaNumeracao(command.FilialId);

            //Gera nova entidade 
            var documentoEntrada = new DocumentoEntrada(
                Guid.NewGuid(),
                filial.Id,
                proximoNumeroDocumentoEntrada,
                command.Data,
                TipoOperacao.FromValue(command.TipoOperacao),
                notaFiscalGraos);

            //Adiciona as entidades ao repositório
            _documentoEntradaRepository.Add(documentoEntrada);

            return new AbreDocumentoEntradaCommandResult(documentoEntrada.Id, proximoNumeroDocumentoEntrada.ToString());
        }

        public ICommandResult Handle(ComplementaDocumentoEntradaComLotesEPesosCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiListaDeLotesInformada()
                  && command.PossuiTodosLotesComNumeroInformado()
                  & command.PossuiTodosLotesComSacasMaiorQueZero()
                  & command.PossuiTodosLotesComPesoMaiorQueZero()))
            {
                return new ComplementaDocumentoEntradaCommandResult();
            }

            //Validações de coisas que vão em repositório
            var documentoEntrada = _documentoEntradaRepository.ObterPorId(command.DocumentoEntradaId);

            if (!(command.PossuiDocumentoEntradaInformado(documentoEntrada) && documentoEntrada.PossuiStatusIgualEmAberto()))
            {
                return new ComplementaDocumentoEntradaCommandResult();
            }

            IEnumerable<Embalagem> embalagens = new List<Embalagem>();

            if (command.PossuiAlgumLoteComEmbalagemInformada())
            {
                embalagens = _embalagemRepository.ObterPorListaDeIds(command.ListaDeEmbalagemIdsToString());

                var lotesOk = true;

                foreach (var lote in command.Lotes)
                {
                    lotesOk = lotesOk & lote.PossuiEmbalagemOpcionalInformada(embalagens);
                }

                if (!lotesOk) return new ComplementaDocumentoEntradaCommandResult();
            }

            IEnumerable<TicketPesagemMovimentacao> ticketsPesagemMovimentacao = new List<TicketPesagemMovimentacao>();

            if (command.PossuiAlgumLoteComTicketPesagemMovimentacaoInformado())
            {
                ticketsPesagemMovimentacao =
                    _ticketPesagemMovimentacaoRepository.ObterPorListaDeIds(
                        command.ListaDeTicketPesagemMovimentacaoIdsToString());

                var lotesOk = true;

                foreach (var lote in command.Lotes)
                {
                    lotesOk = lotesOk & lote.PossuiTicketPesagemMovimentacaoOpcionalInformado(ticketsPesagemMovimentacao);
                }

                if (!lotesOk) return new ComplementaDocumentoEntradaCommandResult();
            }

            //Gera novas entidades
            foreach (var commandLote in command.Lotes)
            {
                var lote = new Lote(Guid.NewGuid(), documentoEntrada.FilialId, 
                    commandLote.Numero, commandLote.Sacas, commandLote.Peso,
                    TipoGrao.FromValue(documentoEntrada.NotaFiscalGraos.TipoGrao),
                    embalagens.FirstOrDefault(e => e.Id == commandLote.EmbalagemId),
                    ticketsPesagemMovimentacao.FirstOrDefault(t => t.Id == commandLote.TicketPesagemMovimentacaoId),
                    documentoEntrada.NotaFiscalGraos.DestinatarioCadastroId,
                    documentoEntrada.NotaFiscalGraos.DestinatarioFazendaId,
                    documentoEntrada);

                documentoEntrada.Lotes.Add(lote);
            }

            documentoEntrada.TrocaStatusParaComplementadoComLotesEPesos();

            //Atualiza a entidade no repositório
            var documentoEntradaAtualizado = _documentoEntradaRepository.Update(documentoEntrada);

            return new ComplementaDocumentoEntradaCommandResult();
        }

        public ICommandResult Handle(ComplementaDocumentoEntradaComPesosCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório

            //Validações de coisas que vão em repositório

            //Gera novas entidades

            //Atualiza a entidade no repositório

            return new ComplementaDocumentoEntradaCommandResult();
        }
    }
}