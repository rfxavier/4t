using s4t.Erp.Cadastros.Data.Context;
using s4t.Erp.Cadastros.Data.Repository;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.Enums;
using s4t.Erp.Core.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace s4t.Erp.Cadastros.Tests.Integration
{
    public class FazendaTests
    {
        [Fact]
        [Trait("Category", "Integration Fazenda")]
        public void FazendaInsertTests()
        {
            using (var repo = new FazendaRepository(new CadastrosContext()))
            {
                var j = FazendaStatus.List().Select(x => new {Codigo = x.Value, Descricao = x.Name}).ToList();

                var cadastro = new Cadastro(Guid.NewGuid(), 1, "Cadastro 1", TipoPFPJ.PF, new CNPJ(""), new CPF(""), "",
                    "", "", "", "", "", "", new CEP("37950000"), new Cidade(Guid.NewGuid(), 1, "1", "Cidade 1",
                        new UF(Guid.NewGuid(), 1, "UF 1", "1", new Pais(Guid.NewGuid(), 1, "País 1", "1"))),
                    new Email(""), DateTime.Now, "");

                var fazenda = new Fazenda(Guid.NewGuid(), 1, "Fazenda Teste 2", new CNPJ(""), DateTime.Now,
                    DateTime.Now, "", "", "", "", "", "", new CEP(""),
                    new Cidade(Guid.NewGuid(), 1, "1", "Cidade 2",
                        new UF(Guid.NewGuid(), 1, "UF 2", "1", new Pais(Guid.NewGuid(), 1, "País 2", "1"))),
                    new Telefone(""), new Telefone(""), "", DateTime.Now,
                    FazendaStatus.Inativa, cadastro);

                repo.Add(fazenda);
                repo.SaveChanges();
            }
        }
    }
}