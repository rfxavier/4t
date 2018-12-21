using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Pais : Entity
    {
        public Pais(Guid id, int codigo, string nome, string codigoIbge)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            CodigoIbge = codigoIbge;
        }
        // Empty constructor for EF
        protected Pais() { }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CodigoIbge { get; private set; }
    }
}