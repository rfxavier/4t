using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Tests.Unit.Balanca.Entities.Builders;
using System;
using System.Linq;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Balanca.Entities
{
    public class TicketPesagemTests
    {
        private readonly TicketPesagem _ticketPesagem;

        public TicketPesagemTests()
        {
            _ticketPesagem = new TicketPesagemBuilder();

        }

        [Theory(DisplayName = "AdicionaPesagemMovimentacao")]
        [InlineData(1927.55, 1927.6)]
        [InlineData(1000.0000001, 1000)]
        [InlineData(1737.999, 1738)]
        [InlineData(1374, 1374)]
        [InlineData(1545.3, 1545.3)]
        [InlineData(1122.34, 1122.3)]
        [Trait("Category", "TicketPesagem Entity")]
        void
            TicketPesagem_AoAdicionarPesagemMovimentacao_AdicionaPesagemMovimentacao_DeveGravarPeso_ComUmaCasaDecimal(double pesoPassado, double pesoEsperado)
        {
            //Arrange
            var ticketPesagemMovimentacao = new TicketPesagemMovimentacao(Guid.NewGuid(), pesoPassado);

            //Act
            var ticketPesagemMovimentacaoAposAdicao = _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao);

            //Assert
            Assert.Equal(pesoEsperado, ticketPesagemMovimentacaoAposAdicao.Peso);
        }


        [Fact(DisplayName = "AdicionaPesagemMovimentacao")]
        [Trait("Category", "TicketPesagem Entity")]
        void TicketPesagem_AoAdicionarPesagemMovimentacao_AdicionaPesagemMovimentacao_DeveGravarCorretamente_PesoDiferencialAnterior()
        {
            //Arrange
            var ticketPesagemMovimentacao1 = new TicketPesagemMovimentacao(Guid.NewGuid(), 1740.38);
            var ticketPesagemMovimentacao2 = new TicketPesagemMovimentacao(Guid.NewGuid(), 1630.23);
            var ticketPesagemMovimentacao3 = new TicketPesagemMovimentacao(Guid.NewGuid(), 1927.55);

            //Act
            //Pesagem 1
            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao1);

            // ReSharper disable once PossibleNullReferenceException
            double pesoDiferencialAnterior1 = _ticketPesagem.TicketPesagemMovimentacoes.OrderByDescending(m => m.Ordem).FirstOrDefault().PesoDiferencialAnterior;

            //Pesagem 2
            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao2);

            // ReSharper disable once PossibleNullReferenceException
            double pesoDiferencialAnterior2 = _ticketPesagem.TicketPesagemMovimentacoes.OrderByDescending(m => m.Ordem).FirstOrDefault().PesoDiferencialAnterior;

            //Pesagem 3
            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao3);

            // ReSharper disable once PossibleNullReferenceException
            double pesoDiferencialAnterior3 = _ticketPesagem.TicketPesagemMovimentacoes.OrderByDescending(m => m.Ordem).FirstOrDefault().PesoDiferencialAnterior;

            //Assert
            Assert.Equal(1740.4, pesoDiferencialAnterior1);
            Assert.Equal(110.2, pesoDiferencialAnterior2);
            Assert.Equal(297.4, pesoDiferencialAnterior3);
        }

        [Fact(DisplayName = "AdicionaPesagemMovimentacao")]
        [Trait("Category", "TicketPesagem Entity")]
        void TicketPesagem_AoAdicionarPesagemMovimentacao_AdicionaPesagemMovimentacao_DeveRetornarCorretamente_Ordem()
        {
            //Arrange
            var ticketPesagemMovimentacao1 = new TicketPesagemMovimentacao(Guid.NewGuid(), 0);
            var ticketPesagemMovimentacao2 = new TicketPesagemMovimentacao(Guid.NewGuid(), 0);
            var ticketPesagemMovimentacao3 = new TicketPesagemMovimentacao(Guid.NewGuid(), 0);

            //Act
            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao1);
            var ordemMovimentacao1 = ticketPesagemMovimentacao1.Ordem;

            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao2);
            var ordemMovimentacao2 = ticketPesagemMovimentacao2.Ordem;

            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao3);
            var ordemMovimentacao3 = ticketPesagemMovimentacao3.Ordem;

            //Assert
            Assert.Equal(1, ordemMovimentacao1);
            Assert.Equal(2, ordemMovimentacao2);
            Assert.Equal(3, ordemMovimentacao3);
        }

        [Fact(DisplayName = "AdicionaPesagemMovimentacao")]
        [Trait("Category", "TicketPesagem Entity")]
        void TicketPesagem_AdicionaPesagemMovimentacao_DeveAdicionarPesagemMovimentacao_Corretamente()
        {
            //Arrange
            var ticketPesagemMovimentacao1 = new TicketPesagemMovimentacao(Guid.NewGuid(), 0);

            var ticketPesagemContagemMovimentacoes = _ticketPesagem.TicketPesagemMovimentacoes.Count;

            //Act
            _ticketPesagem.AdicionaPesagemMovimentacao(ticketPesagemMovimentacao1);

            //Assert
            Assert.True(_ticketPesagem.TicketPesagemMovimentacoes.Count == ticketPesagemContagemMovimentacoes + 1);
        }

    }
}