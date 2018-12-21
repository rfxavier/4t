using s4t.Erp.Graos.Domain.Balanca.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Commands.Builders
{
    public class ContinuaPesagemRegistraPesoCommandBuilder
    {
        private Guid _ticketPesagemId = Guid.Empty;
        private double _peso = 0;

        public ContinuaPesagemRegistraPesoCommand Build()
        {
            return new ContinuaPesagemRegistraPesoCommand(_ticketPesagemId, _peso);
        }

        public ContinuaPesagemRegistraPesoCommandBuilder ComTicketPesagemId(Guid ticketPesagemId)
        {
            this._ticketPesagemId = ticketPesagemId;
            return this;
        }

        public ContinuaPesagemRegistraPesoCommandBuilder ComPeso(double peso)
        {
            this._peso = peso;
            return this;
        }

        public static implicit operator ContinuaPesagemRegistraPesoCommand(ContinuaPesagemRegistraPesoCommandBuilder instance)
        {
            return instance.Build();
        }



    }
}