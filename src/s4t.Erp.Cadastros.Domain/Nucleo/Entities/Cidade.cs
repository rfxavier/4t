using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Cidade : Entity
    {
        public Cidade(Guid id, int codigo, string codigoIbge, string nome, UF uf)
        {
            Id = id;
            Codigo = codigo;
            CodigoIbge = codigoIbge;
            Nome = nome;
            Uf = uf;
        }
        // Empty constructor for EF
        protected Cidade() { }

        public int Codigo { get; private set; }
        public string CodigoIbge { get; private set; }
        public string Nome { get; private set; }
        public UF Uf { get; private set; }
    }
}