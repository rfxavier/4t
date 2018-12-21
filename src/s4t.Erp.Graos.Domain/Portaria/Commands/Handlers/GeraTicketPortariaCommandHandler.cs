using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Core.Domain.ValueObjects.Placa;
using s4t.Erp.Graos.Domain.Core.CommandHandler;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using s4t.Erp.Graos.Domain.Portaria.Commands.Results;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using s4t.Erp.Graos.Domain.Portaria.Events.RegistroDePortariaEvents;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using System;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Handlers
{
    public class GeraTicketPortariaCommandHandler : CommandHandler,
        ICommandHandler<GeraTicketPortariaDesembarqueParaEntradaDepositoCommand>,
        ICommandHandler<GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand>
    {
        private readonly IFilialRepository _filialRepository;
        private readonly IRegistroDePortariaRepository _registroDePortariaRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly ITicketPortariaRepository _ticketRepository;

        public GeraTicketPortariaCommandHandler(
            IFilialRepository filialRepository,
            IRegistroDePortariaRepository registroDePortariaRepository,
            IMotoristaRepository motoristaRepository,
            INotaFiscalRepository notaFiscalRepository,
            ITicketPortariaRepository ticketRepository,
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _filialRepository = filialRepository;
            _registroDePortariaRepository = registroDePortariaRepository;
            _motoristaRepository = motoristaRepository;
            _notaFiscalRepository = notaFiscalRepository;
            _ticketRepository = ticketRepository;
        }

        public ICommandResult Handle(GeraTicketPortariaDesembarqueParaEntradaDepositoCommand command)
        {
            //Gera os Value Objects
            var placa = new Placa(command.PlacaNumero);

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiTipoGraoValido()
                  & command.PossuiTipoOperacaoValido()
                  & placa.EstaValidaEPreenchida()))
            {
                return new GeraTicketCommandResult("");
            }

            //Validações de coisas que vão em repositório
            var filial = _filialRepository.ObterPorId(command.FilialId);

            var motorista = _motoristaRepository.GetById(command.MotoristaId);

            var listaDeNotaFiscalIds = command.NotasFiscais.Select(x => x.NotaFiscalId).ToList();
            var notasFiscais = _notaFiscalRepository.GetByListaDeIds(listaDeNotaFiscalIds);

            if (!(command.PossuiFilialInformada(filial) 
                & command.PossuiMotoristaComCadastroPreenchido(motorista)
                & command.PossuiNotasFiscaisDesembarqueParaEntradaDepositoInformadas(notasFiscais)))
            {
                return new GeraTicketCommandResult("");
            }

            //Gera nova entidade RegistroDePortaria
            var registroDePortaria = new RegistroDePortaria(
                Guid.NewGuid(),
                filial.Id,
                TipoGrao.FromValue(command.TipoGrao), 
                TipoOperacaoPortaria.FromValue(command.TipoOperacaoPortaria),
                placa, 
                motorista,
                DateTime.Now);
            //todo refactor commands as dtos: fixar TipoOperacaoPortaria, implementar testes

            foreach (var notaFiscal in notasFiscais)
            {
                registroDePortaria.AdicionaNotaFiscal(notaFiscal);
            }

            //Gera nova entidade TicketPortaria
            var numeroTicket = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            var ticket = new TicketPortaria(Guid.NewGuid(), filial.Id, numeroTicket, registroDePortaria);
            //todo teste se registroDePortaria.filialId = TicketPortaria.filialId

            //Dispara evento SendoCriado = antes de Commit, dentro da mesma transação
            //Antes do .Add, porque evento RegistroDePortariaSendoCriadoEvent vai também
            //ser assinado por quem vai tratar de vincular certificações se houver
            DomainEvent.Raise(new RegistroDePortariaSendoCriadoEvent(registroDePortaria, filial));

            //Adiciona as entidades ao repositório 
            _registroDePortariaRepository.Add(registroDePortaria);
            _ticketRepository.Add(ticket);


            if (Commit())
            {
                return new GeraTicketCommandResult(ticket.Numero);
            }

            return new GeraTicketCommandResult("");
        }

        public ICommandResult Handle(GeraTicketPortariaDesembarqueParaEntradaTransferenciaCommand command)
        {
            //Gera os Value Objects
            var placa = new Placa(command.PlacaNumero);

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiTipoGraoValido()
                  & command.PossuiTipoOperacaoValido()
                  & placa.EstaValidaEPreenchida()))
            {
                return new GeraTicketCommandResult("");
            }

            //Validações de coisas que vão em repositório
            var filial = _filialRepository.ObterPorId(command.FilialId);

            var motorista = _motoristaRepository.GetById(command.MotoristaId);

            var listaDeNotaFiscalIds = command.NotasFiscais.Select(x => x.NotaFiscalId).ToList();
            var notasFiscais = _notaFiscalRepository.GetByListaDeIds(listaDeNotaFiscalIds);

            if (!(command.PossuiFilialInformada(filial)
                & command.PossuiMotoristaComCadastroPreenchido(motorista)
                & command.PossuiNotasFiscaisDesembarqueParaEntradaTransferenciaInformadas(notasFiscais)))
            {
                return new GeraTicketCommandResult("");
            }

            //Gera nova entidade RegistroDePortaria
            var registroDePortaria = new RegistroDePortaria(
                Guid.NewGuid(),
                filial.Id,
                TipoGrao.FromValue(command.TipoGrao), 
                TipoOperacaoPortaria.FromValue(command.TipoOperacaoPortaria),
                placa, 
                motorista,
                DateTime.Now);

            foreach (var notaFiscal in notasFiscais)
            {
                registroDePortaria.AdicionaNotaFiscal(notaFiscal);
            }

            //Gera nova entidade TicketPortaria
            var numeroTicket = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            var ticket = new TicketPortaria(Guid.NewGuid(), filial.Id, numeroTicket, registroDePortaria);
            //todo teste se registroDePortaria.filialId = TicketPortaria.filialId

            //Adiciona as entidades ao repositório 
            _registroDePortariaRepository.Add(registroDePortaria);
            _ticketRepository.Add(ticket);

            //Dispara evento SendoCriado = antes de Commit, dentro da mesma transação
            DomainEvent.Raise(new RegistroDePortariaSendoCriadoEvent(registroDePortaria, filial));

            if (Commit())
            {
                return new GeraTicketCommandResult(ticket.Numero);
            }

            return new GeraTicketCommandResult("");
        }
    }
}