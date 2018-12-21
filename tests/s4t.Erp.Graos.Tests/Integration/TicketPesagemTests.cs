using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Data.Repository;
using s4t.Erp.Graos.Data.UoW;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Integration
{
    public class TicketPesagemTests
    {
        [Fact]
        [Trait("Category", "Integration TicketPesagem")]
        public void TicketPesagem_InsertTests()
        {
            using (var context = new GraosContext())
            {
                var ticketPesagemId = Guid.NewGuid();
                var repoTicketPesagem = new TicketPesagemRepository(context);

                using (var uow = new UnitOfWork(context))
                {
                    //Gera novo ticket e primeira movimentação de pesagem
                    var ticketPesagem = new TicketPesagem(ticketPesagemId, Guid.NewGuid(), "1234", null as DocumentoEntrada);

                    var ticketPesagemMovimentacao = new TicketPesagemMovimentacao(Guid.NewGuid(), 1000);

                    ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);

                    repoTicketPesagem.Add(ticketPesagem);

                    uow.Commit();

                    //Recupera ticket, adiciona mais uma movimentação de pesagem
                    var ticketPesagemRecuperado = repoTicketPesagem.GetById(ticketPesagemId);

                    var ticketPesagemMovimentacaoNova = new TicketPesagemMovimentacao(Guid.NewGuid(), 1350);

                    ticketPesagemRecuperado.AdicionaPesagemMovimentacao(ticketPesagemMovimentacaoNova);

                    repoTicketPesagem.Update(ticketPesagemRecuperado);

                    uow.Commit();
                }
            }
        }
    }
}