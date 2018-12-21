using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class Motorista : Entity
    {
        //Compartilhado com Cadastro
        public Motorista(Guid id, int codigo, string nome, CPF cpf)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
        }

        // Empty constructor for EF
        protected Motorista() { }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public CPF Cpf { get; private set; }
    }
}