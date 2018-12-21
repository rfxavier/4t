using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities
{
    public class DocumentoEntradaEmbalagem : Entity
    {
        public DocumentoEntradaEmbalagem(Guid id, Embalagem embalagem, int quantidade)
        {
            Id = id;
            Embalagem = embalagem;
            Quantidade = quantidade;
            DocumentoEntradaId = Guid.Empty;
            DocumentoEntrada = null;
        }

        public Guid DocumentoEntradaId { get; private set; }
        public DocumentoEntrada DocumentoEntrada { get; private set; }
        public Embalagem Embalagem { get; private set; }
        public int Quantidade { get; private set; }
    }
}
