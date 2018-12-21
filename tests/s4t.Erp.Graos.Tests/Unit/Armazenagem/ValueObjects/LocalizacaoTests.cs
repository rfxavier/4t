using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects.Builders;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.ValueObjects
{
    public class LocalizacaoTests
    {
        [Fact(DisplayName = "Localizacao Tipo()")]
        [Trait("Category", "Localizacao Value Object")]
        void Localizacao_ComArmazem_Tipo_DeveRetornarIgualAPilha()
        {
            //Arrange
            Localizacao localizacao = null;

            //Act
            localizacao = new LocalizacaoBuilder()
                .ComArmazem(new ArmazemBuilder()
                    .ComArmazemId(Guid.NewGuid()));

            //Assert
            Assert.Equal(LocalizacaoTipo.Pilha, localizacao.Tipo());
        }

        [Fact(DisplayName = "Localizacao Tipo()")]
        [Trait("Category", "Localizacao Value Object")]
        void Localizacao_SemArmazem_Tipo_DeveRetornarIgualALocal()
        {
            //Arrange
            Localizacao localizacao = null;

            //Act
            localizacao = (Localizacao) new LocalizacaoBuilder()
                .ComArmazem(null);

            //Assert
            Assert.Equal(LocalizacaoTipo.Local, localizacao.Tipo());
        }

        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "Localizacao Value Object")]
        void Localizacao_ComArmazemLocalEPilhaNulos_Construtor_DeveInstanciar_ArmazemIdLocalIdPilhaIdVazios()
        {
            //Arrange
            Localizacao localizacao = null;

            //Act
            localizacao = new Localizacao(Guid.Empty, null, string.Empty, String.Empty, null, null);

            //Assert
            Assert.Equal(Guid.Empty, localizacao.ArmazemId);
            Assert.Equal(Guid.Empty, localizacao.LocalId);
            Assert.Equal(Guid.Empty, localizacao.PilhaId);
        }

        [Fact(DisplayName = "Construtor")]
        [Trait("Category", "Localizacao Value Object")]
        void Localizacao_ComArmazemLocalEPilhaPreenchidos_Construtor_DeveInstanciar_ArmazemIdLocalIdPilhaIdCorretamente()
        {
            //Arrange
            Localizacao localizacao = null;
            var armazemId = Guid.NewGuid();
            var localId = Guid.NewGuid();
            var pilhaId = Guid.NewGuid();

            //Act
            localizacao = new Localizacao(Guid.Empty,
                new ArmazemBuilder()
                    .ComArmazemId(armazemId),
                string.Empty,
                String.Empty,
                new LocalBuilder()
                    .ComLocalId(localId),
                new PilhaBuilder()
                    .ComPilhaId(pilhaId));

            //Assert
            Assert.Equal(armazemId, localizacao.ArmazemId);
            Assert.Equal(localId, localizacao.LocalId);
            Assert.Equal(pilhaId, localizacao.PilhaId);
        }
    }
}