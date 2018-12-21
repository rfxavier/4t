using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Empresa: Entity
    {
        private readonly IList<Filial> _filiais;

        public Empresa(Guid id, int codigo, string nome)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;

            _filiais = new List<Filial>();
        }

        // Empty constructor for EF
        protected Empresa() { }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public ICollection<Filial> Filiais
        {
            get { return _filiais; }
        }

        public void AdicionaFilial(Filial filial)
        {
            _filiais.Add(filial);
        }

    }
}
