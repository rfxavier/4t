using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Nucleo.Entities
{
    public class Embalagem : Entity
    {
        public Embalagem(Guid id, int codigo, string descricao, double capacidade, double peso)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Capacidade = capacidade;
            Peso = peso;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public double Capacidade { get; private set; }
        public double Peso { get; private set; }
    }
}