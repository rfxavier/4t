using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;

namespace s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEventHandlers
{
    public class RegistroDePortariaServicoAvulsoSendoCriadoHandlerParaLog : IHandler<RegistroDePortariaServicoAvulsoSendoCriadoEvent>
    {
        private readonly IRegistroDePortariaLogRepository _registroDePortariaLogRepository;

        public RegistroDePortariaServicoAvulsoSendoCriadoHandlerParaLog(IRegistroDePortariaLogRepository registroDePortariaLogRepository)
        {
            _registroDePortariaLogRepository = registroDePortariaLogRepository;
        }

        public void Handle(RegistroDePortariaServicoAvulsoSendoCriadoEvent args)
        {
            var motoristaCodigo = args.RegistroDePortariaServicoAvulso.Motorista == null ? 0 : args.RegistroDePortariaServicoAvulso.Motorista.Codigo;
            var motoristaNome = args.RegistroDePortariaServicoAvulso.Motorista == null ? "" : args.RegistroDePortariaServicoAvulso.Motorista.Nome;

            var registroDePortariaLog = new RegistroDePortariaLog
            {
                RegistroDePortariaServicoAvulsoId = args.RegistroDePortariaServicoAvulso.Id,
                FilialCodigo = args.Filial.Codigo,
                FilialNome = args.Filial.Nome,
                Placa = args.RegistroDePortariaServicoAvulso.Placa.Numero,
                MotoristaCodigo = motoristaCodigo,
                MotoristaNome = motoristaNome,
                NomeDoMotoristaSemCadastro = args.RegistroDePortariaServicoAvulso.NomeDoMotoristaSemCadastro,
                ProdutoPortariaCodigo = args.RegistroDePortariaServicoAvulso.ProdutoPortaria.Codigo,
                ProdutoPortariaDescricao = args.RegistroDePortariaServicoAvulso.ProdutoPortaria.Descricao,
                Data = args.RegistroDePortariaServicoAvulso.Data
            };

            _registroDePortariaLogRepository.Add(registroDePortariaLog);
        }
    }
}