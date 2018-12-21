using s4t.Erp.Graos.Domain.Nucleo.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Nucleo
{
    public class TipoGraoDadosTeste : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {1, "Café", TipoGrao.Cafe.Value, TipoGrao.Cafe.Name},
            new object[]
                {2, "Milho", TipoGrao.Milho.Value, TipoGrao.Milho.Name},
            new object[]
                {3, "Soja", TipoGrao.Soja.Value, TipoGrao.Soja.Name},
            new object[]
                {TipoGrao.Cafe.Value, TipoGrao.Cafe.Name, FazendaCertificacaoTipoCultura.Cafe.Value, FazendaCertificacaoTipoCultura.Cafe.Name},
            new object[]
                {TipoGrao.Milho.Value, TipoGrao.Milho.Name, FazendaCertificacaoTipoCultura.Milho.Value, FazendaCertificacaoTipoCultura.Milho.Name},
            new object[]
                {TipoGrao.Soja.Value, TipoGrao.Soja.Name, FazendaCertificacaoTipoCultura.Soja.Value, FazendaCertificacaoTipoCultura.Soja.Name},

        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class EnumEquivalenciaTests
    {
        [Theory(DisplayName = "TipoGrao FazendaCertificacaoTipoCultura")]
        [ClassData(typeof(TipoGraoDadosTeste))]
        [Trait("Category", "Enums")]
        void FazendaCertificacaoTipoCultura_TipoGrao_Enums_SaoEquivalentes(
            int codigo, string descricao,
            int codigoEsperado, string descricaoEsperada)
        {
            //Arrange

            //Act

            //Assert
            Assert.Equal(codigo, codigoEsperado);
            Assert.Equal(descricao, descricaoEsperada);
            Assert.Equal(3, TipoGrao.List().Count());
        }
    }
}