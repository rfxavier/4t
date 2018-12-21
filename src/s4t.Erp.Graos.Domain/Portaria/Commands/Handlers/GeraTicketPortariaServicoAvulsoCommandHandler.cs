using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Core.Domain.ValueObjects.Placa;
using s4t.Erp.Graos.Domain.Core.CommandHandler;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Commands.Results;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Handlers
{
    public class GeraTicketPortariaServicoAvulsoCommandHandler : CommandHandler,
        ICommandHandler<GeraTicketPortariaServicoAvulsoCommand>
    {
        private readonly IFilialRepository _filialRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IProdutoPortariaRepository _produtoPortariaRepository;
        private readonly IRegistroDePortariaServicoAvulsoRepository _registroDePortariaServicoAvulsoRepository;
        private readonly ITicketPortariaRepository _ticketRepository;

        public GeraTicketPortariaServicoAvulsoCommandHandler(
            IFilialRepository filialRepository,
            IMotoristaRepository motoristaRepository,
            IProdutoPortariaRepository produtoPortariaRepository,
            IRegistroDePortariaServicoAvulsoRepository registroDePortariaServicoAvulsoRepository,
            ITicketPortariaRepository ticketRepository,
            IUnitOfWork uow, IDomainNotificationHandler<DomainNotification> notifications) :
            base(uow, notifications)
        {
            _filialRepository = filialRepository;
            _motoristaRepository = motoristaRepository;
            _produtoPortariaRepository = produtoPortariaRepository;
            _registroDePortariaServicoAvulsoRepository = registroDePortariaServicoAvulsoRepository;
            _ticketRepository = ticketRepository;
        }

        public ICommandResult Handle(GeraTicketPortariaServicoAvulsoCommand command)
        {
            //Gera os Value Objects
            var placa = new Placa(command.PlacaNumero);

            //Validações de coisas que não vão em repositório
            if (!(placa.EstaValida()))
            {
                return new GeraTicketCommandResult("");
            }

            //Validações de coisas que vão em repositório
            var filial = _filialRepository.ObterPorId(command.FilialId);

            var motorista = _motoristaRepository.GetById(command.MotoristaId);

            var produtoPortaria = _produtoPortariaRepository.GetById(command.ProdutoPortariaId);

            if (!(command.PossuiFilialInformada(filial) 
                & command.PossuiProdutoPortariaInformado(produtoPortaria)))
            {
                return new GeraTicketCommandResult("");
            }

            //Gera nova entidade RegistroDePortariaServicoAvulso
            var registroDePortariaServicoAvulso = new RegistroDePortariaServicoAvulso(Guid.NewGuid(), filial.Id,
                placa, motorista, command.NomeDoMotoristaSemCadastro, produtoPortaria,
                DateTime.Now);

            //Gera nova entidade TicketPortaria
            var numeroTicket = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            var ticket = new TicketPortaria(Guid.NewGuid(), filial.Id, numeroTicket, registroDePortariaServicoAvulso);
            //todo teste se registroDePortariaServicoAvulso.filialId = TicketPortaria.filialId

            //Adiciona as entidades ao repositório 
            _registroDePortariaServicoAvulsoRepository.Add(registroDePortariaServicoAvulso);
            _ticketRepository.Add(ticket);

            //Dispara evento SendoCriado = antes de Commit, dentro da mesma transação
            DomainEvent.Raise(new RegistroDePortariaServicoAvulsoSendoCriadoEvent(registroDePortariaServicoAvulso, filial));

            if (Commit())
            {
                return new GeraTicketCommandResult(ticket.Numero);
            }

            return new GeraTicketCommandResult("");
        }
    }
}