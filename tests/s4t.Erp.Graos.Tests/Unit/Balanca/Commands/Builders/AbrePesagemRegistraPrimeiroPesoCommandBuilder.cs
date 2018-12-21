using s4t.Erp.Graos.Domain.Balanca.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Commands.Builders
{
    public class AbrePesagemRegistraPrimeiroPesoCommandBuilder
    {
        private Guid _documentoEntradaId = Guid.Empty;
        private double _peso = 0;

        public AbrePesagemRegistraPrimeiroPesoCommand Build()
        {
            return new AbrePesagemRegistraPrimeiroPesoCommand(_documentoEntradaId, _peso);
        }

        public AbrePesagemRegistraPrimeiroPesoCommandBuilder ComDocumentoEntradaId(Guid documentoEntradaId)
        {
            this._documentoEntradaId = documentoEntradaId;
            return this;
        }

        public AbrePesagemRegistraPrimeiroPesoCommandBuilder ComPeso(double peso)
        {
            this._peso = peso;
            return this;
        }

        public static implicit operator AbrePesagemRegistraPrimeiroPesoCommand(AbrePesagemRegistraPrimeiroPesoCommandBuilder instance)
        {
            return instance.Build();
        }


    }
}