using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects.Placa;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Entities
{
    public class RegistroDePortariaServicoAvulso : Entity
    {
        public RegistroDePortariaServicoAvulso(Guid id, Guid filialId, Placa placa, Motorista motorista,
            string nomeDoMotoristaSemCadastro, ProdutoPortaria produtoPortaria, DateTime data)
        {
            Id = id;
            FilialId = filialId;
            Placa = placa;
            Motorista = motorista;
            NomeDoMotoristaSemCadastro = nomeDoMotoristaSemCadastro;
            ProdutoPortaria = produtoPortaria;
            Data = data;
        }

        public Guid FilialId { get; private set; }
        public Placa Placa { get; private set; }
        public Motorista Motorista { get; private set; }
        public string NomeDoMotoristaSemCadastro { get; private set; }
        public ProdutoPortaria ProdutoPortaria { get; private set; }
        public DateTime Data { get; private set; }
    }
}