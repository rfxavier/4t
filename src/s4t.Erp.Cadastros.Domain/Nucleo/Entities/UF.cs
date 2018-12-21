using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class UF : Entity
    {
        private readonly IList<Cidade> _cidades;

        public UF(Guid id, int codigo, string descricao, string codigoIbge, Pais pais)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            CodigoIbge = codigoIbge;
            Pais = pais;

            _cidades = new List<Cidade>();
        }

        // Empty constructor for EF
        protected UF() { }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string CodigoIbge { get; private set; }
        public Pais Pais { get; private set; }

        public ICollection<Cidade> Cidades
        {
            get { return _cidades; }
        }

        public void AdicionaCidade(Cidade cidade)
        {
            _cidades.Add(cidade);
        }
    }
}