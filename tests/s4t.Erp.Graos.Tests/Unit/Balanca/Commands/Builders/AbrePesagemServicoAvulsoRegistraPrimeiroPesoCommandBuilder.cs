using s4t.Erp.Graos.Domain.Balanca.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Commands.Builders
{
    public class AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder
    {
        private Guid _ticketPortariaId = Guid.Empty;
        private double _peso = 0;

        public AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand Build()
        {
            return new AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand(_ticketPortariaId, _peso);
        }

        public AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder ComTicketPortariaId(Guid ticketPortariaId)
        {
            this._ticketPortariaId = ticketPortariaId;
            return this;
        }

        public AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder ComPeso(double peso)
        {
            this._peso = peso;
            return this;
        }

        public static implicit operator AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommand(AbrePesagemServicoAvulsoRegistraPrimeiroPesoCommandBuilder instance)
        {
            return instance.Build();
        }



    }
}