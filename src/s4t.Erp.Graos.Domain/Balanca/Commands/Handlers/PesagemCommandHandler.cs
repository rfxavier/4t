using s4t.Erp.Core.Domain.Commands;
using s4t.Erp.Core.Domain.DomainNotification.Events;
using s4t.Erp.Graos.Domain.Balanca.Commands.Inputs;
using s4t.Erp.Graos.Domain.Balanca.Commands.Results;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Balanca.Interfaces;
using s4t.Erp.Graos.Domain.Core.CommandHandler;
using s4t.Erp.Graos.Domain.Core.Interfaces;
using s4t.Erp.Graos.Domain.Portaria.Interfaces;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Interfaces;
using System;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Handlers
{
    public class PesagemCommandHandler : CommandHandler,
        ICommandHandler<AbrePesagemRegistraPrimeiroPesoCommand>, 
        ICommandHandler<AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand>,
        ICommandHandler<ContinuaPesagemRegistraPesoCommand>
    {
        private readonly IDocumentoEntradaRepository _documentoEntradaRepository;
        private readonly ITicketPesagemRepository _ticketPesagemRepository;
        private readonly ITicketPortariaRepository _ticketPortariaRepository;

        public PesagemCommandHandler(
            IDocumentoEntradaRepository documentoEntradaRepository,
            ITicketPesagemRepository ticketPesagemRepository,
            ITicketPortariaRepository ticketPortariaRepository,
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _documentoEntradaRepository = documentoEntradaRepository;
            _ticketPesagemRepository = ticketPesagemRepository;
            _ticketPortariaRepository = ticketPortariaRepository;
        }

        public ICommandResult Handle(AbrePesagemRegistraPrimeiroPesoCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiPesoInvalido()))
            {
                return new PesagemCommandResult(string.Empty);
            }

            //Validações de coisas que vão em repositório
            var documentoEntrada = _documentoEntradaRepository.ObterPorId(command.DocumentoEntradaId);

            if (!(command.PossuiDocumentoEntradaInformado(documentoEntrada)))
            {
                return new PesagemCommandResult(string.Empty);
            }

            string proximoNumeroTicketPesagem = _ticketPesagemRepository.GetProximaNumeracao(documentoEntrada.FilialId).ToString();

            //Gera novas entidades
            var ticketPesagem = new TicketPesagem(Guid.NewGuid(), documentoEntrada.FilialId,
                proximoNumeroTicketPesagem,
                documentoEntrada);

            var ticketPesagemMovimentacao = new TicketPesagemMovimentacao(ticketPesagem.Id, command.Peso);

            ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);

            //Adiciona as entidades ao repositório
            TicketPesagem ticketPesagemAdicionado = _ticketPesagemRepository.Add(ticketPesagem);

            return new PesagemCommandResult(ticketPesagemAdicionado == null ? string.Empty : ticketPesagemAdicionado.Numero);
        }

        public ICommandResult Handle(AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiPesoInvalido()))
            {
                return new PesagemCommandResult(string.Empty);
            }

            //Validações de coisas que vão em repositório
            var ticketPortaria = _ticketPortariaRepository.GetById(command.TicketPortariaId);

            if (!(command.PossuiTicketPortariaInformado(ticketPortaria)))
            {
                return new PesagemCommandResult(string.Empty);
            }

            string proximoNumeroTicketPesagem = _ticketPesagemRepository.GetProximaNumeracao(ticketPortaria.FilialId).ToString();

            //Gera novas entidades
            var ticketPesagem = new TicketPesagem(Guid.NewGuid(), ticketPortaria.FilialId,
                proximoNumeroTicketPesagem,
                ticketPortaria);

            var ticketPesagemMovimentacao = new TicketPesagemMovimentacao(ticketPesagem.Id, command.Peso);

            ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);

            //Adiciona as entidades ao repositório
            TicketPesagem ticketPesagemAdicionado = _ticketPesagemRepository.Add(ticketPesagem);

            return new PesagemCommandResult(ticketPesagemAdicionado == null ? string.Empty : ticketPesagemAdicionado.Numero);
        }

        public ICommandResult Handle(ContinuaPesagemRegistraPesoCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.PossuiPesoInvalido()))
            {
                return new PesagemCommandResult(string.Empty);
            }

            //Validações de coisas que vão em repositório
            var ticketPesagem = _ticketPesagemRepository.GetById(command.TicketPesagemId);

            if (!(command.PossuiTicketPesagemInformado(ticketPesagem)))
            {
                return new PesagemCommandResult(string.Empty);
            }

            //Gera novas entidades
            var ticketPesagemMovimentacao = new TicketPesagemMovimentacao(ticketPesagem.Id, command.Peso);

            ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);

            //Atualiza a entidade no repositório
            var ticketPesagemAtualizado = _ticketPesagemRepository.Update(ticketPesagem);

            return new PesagemCommandResult(ticketPesagemAtualizado == null ? string.Empty : ticketPesagemAtualizado.Numero);

        }
    }
}