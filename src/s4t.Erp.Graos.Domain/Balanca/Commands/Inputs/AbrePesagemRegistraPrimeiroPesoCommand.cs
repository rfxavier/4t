using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Inputs
{
    public class AbrePesagemRegistraPrimeiroPesoCommand : ICommand
    {
        public AbrePesagemRegistraPrimeiroPesoCommand(Guid documentoEntradaId, double peso)
        {
            DocumentoEntradaId = documentoEntradaId;
            Peso = peso;
        }

        public Guid DocumentoEntradaId { get; private set; }
        public Double Peso { get; private set; }
    }
}