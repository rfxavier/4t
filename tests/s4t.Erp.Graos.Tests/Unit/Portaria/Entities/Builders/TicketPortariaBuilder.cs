using s4t.Erp.Graos.Domain.Portaria.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders
{
    public class TicketPortariaBuilder
    {
        private Guid _ticketId = Guid.Empty;
        private Guid _filialId = Guid.Empty;
        private string _numero = string.Empty;

        private RegistroDePortaria _registroDePortaria;
        private RegistroDePortariaServicoAvulso _registroDePortariaServicoAvulso;

        public TicketPortaria Build()
        {
            if (_registroDePortaria != null)
            {
                return new TicketPortaria(_ticketId, _filialId, _numero, _registroDePortaria);

            }

            if (_registroDePortariaServicoAvulso != null)
            {
                return new TicketPortaria(_ticketId, _filialId, _numero, _registroDePortariaServicoAvulso);

            }

            return new TicketPortaria(_ticketId, _filialId, _numero, _registroDePortaria);
        }

        public TicketPortariaBuilder ComTicketPortariaId(Guid ticketId)
        {
            this._ticketId = ticketId;
            return this;
        }

        public TicketPortariaBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public TicketPortariaBuilder ComNumero(string numero)
        {
            this._numero = numero;
            return this;
        }

        public TicketPortariaBuilder ComRegistroDePortaria(RegistroDePortaria registroDePortaria)
        {
            this._registroDePortaria = registroDePortaria;
            return this;
        }

        public TicketPortariaBuilder ComRegistroDePortariaServicoAvulso(RegistroDePortariaServicoAvulso registroDePortariaServicoAvulso)
        {
            this._registroDePortariaServicoAvulso = registroDePortariaServicoAvulso;
            return this;
        }

        public static implicit operator TicketPortaria(TicketPortariaBuilder instance)
        {
            return instance.Build();
        }
    }
}