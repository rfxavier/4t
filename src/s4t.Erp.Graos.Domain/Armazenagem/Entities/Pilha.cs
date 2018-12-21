using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Entities
{
    public class Pilha : Entity
    {
        public Pilha(Guid id, Guid filialId, Guid armazemId, string quadra, string bloco, bool montaPilha, int numeroBase,
            int numeroAltura, int numeroLastro)
        {
            Id = id;
            FilialId = filialId;
            ArmazemId = armazemId;
            Quadra = quadra;
            Bloco = bloco;
            MontaPilha = montaPilha;
            NumeroBase = numeroBase;
            NumeroAltura = numeroAltura;
            NumeroLastro = numeroLastro;
        }

        public Guid FilialId { get; private set; }
        public Guid ArmazemId { get; private set; }
        public string Quadra { get; private set; }
        public string Bloco { get; private set; }
        public bool MontaPilha { get; private set; }
        public int NumeroBase { get; private set; }
        public int NumeroAltura { get; private set; }
        public int NumeroLastro { get; private set; }
    }
}