using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.ValueObjects
{
    public class Localizacao : ValueObject<Localizacao>
    {
        public Guid FilialId { get; protected set; }
        public Guid ArmazemId { get; protected set; }
        public Armazem Armazem { get; private set; }
        public string Quadra { get; protected set; } 
        public string Bloco { get; protected set; }
        public Guid LocalId { get; protected set; }
        public Local Local { get; private set; }
        public Guid PilhaId { get; private set; }
        public Pilha Pilha { get; private set; }

        protected Localizacao() { }

        public Localizacao(Guid filialId, Armazem armazem, string quadra, string bloco, Local local, Pilha pilha)
        {
            FilialId = filialId;
            ArmazemId = armazem == null ? Guid.Empty : armazem.Id;
            Armazem = armazem;
            Quadra = quadra;
            Bloco = bloco;
            LocalId = local == null ? Guid.Empty : local.Id;
            Local = local;
            PilhaId = pilha == null ? Guid.Empty : pilha.Id;
            Pilha = pilha;
        }

        public LocalizacaoTipo Tipo()
        {
            return Armazem != null ? LocalizacaoTipo.Pilha : LocalizacaoTipo.Local;
        }
    }
}