using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;

namespace s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEventHandlers
{
    public class RegistroDePortariaSendoCriadoHandlerParaLog : IHandler<RegistroDePortariaSendoCriadoEvent>
    {
        private readonly IRegistroDePortariaLogRepository _registroDePortariaLogRepository;

        public RegistroDePortariaSendoCriadoHandlerParaLog(IRegistroDePortariaLogRepository registroDePortariaLogRepository)
        {
            _registroDePortariaLogRepository = registroDePortariaLogRepository;
        }

        public void Handle(RegistroDePortariaSendoCriadoEvent args)
        {
            var registroDePortariaLog = new RegistroDePortariaLog
            {
                RegistroDePortariaId = args.RegistroDePortaria.Id,
                FilialCodigo = args.Filial.Codigo,
                FilialNome = args.Filial.Nome,
                TipoGrao = args.RegistroDePortaria.TipoGrao.Value,
                TipoGraoDescricao = args.RegistroDePortaria.TipoGrao.Name,
                TipoOperacao = args.RegistroDePortaria.TipoOperacaoPortaria.Value,
                TipoOperacaoDescricao = args.RegistroDePortaria.TipoOperacaoPortaria.Name,
                Placa = args.RegistroDePortaria.Placa.Numero,
                MotoristaCodigo = args.RegistroDePortaria.Motorista.Codigo,
                MotoristaNome = args.RegistroDePortaria.Motorista.Nome,
                Data = args.RegistroDePortaria.Data
            };

            _registroDePortariaLogRepository.Add(registroDePortariaLog);
        }
    }
}