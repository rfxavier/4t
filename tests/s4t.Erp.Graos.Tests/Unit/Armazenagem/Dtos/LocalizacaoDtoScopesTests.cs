using Moq;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Graos.Domain.Armazenagem.Dtos;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders;
using System;
using Xunit;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Dtos
{
    public class LocalizacaoDtoScopesTests
    {
        [Fact(DisplayName = "ObterLocalizacao")]
        [Trait("Category", "LocalizacaoDto Scopes")]
        void
            LocalizacaoDto_ComArmazemQueNaoAcheFilialArmazemPilha_ObterLocalizacao_DeveRetornar_LocalizacaoValueObject_ComFilialArmazemPilha_Vazios()
        {
            //Arrange
            var filialRepositoryMock = new Mock<IFilialRepository>();
            var armazemRepositoryMock = new Mock<IArmazemRepository>();
            var localRepositoryMock = new Mock<ILocalRepository>();
            var pilhaRepositoryMock = new Mock<IPilhaRepository>();

            filialRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => null);

            armazemRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => null);

            localRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => null);

            pilhaRepositoryMock.Setup(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => null);

            var localizacaoDto = new LocalizacaoDto()
            {
                ArmazemCodigo = "18A"
            };

            //Act
            var localizacao = localizacaoDto.ObterLocalizacao(filialRepositoryMock.Object, armazemRepositoryMock.Object,
                localRepositoryMock.Object, pilhaRepositoryMock.Object);

            //Assert
            Assert.Equal(Guid.Empty, localizacao.FilialId);
            Assert.Null(localizacao.Armazem);
            Assert.Equal(Guid.Empty, localizacao.ArmazemId);
            Assert.Null(localizacao.Local);
            Assert.Equal(Guid.Empty, localizacao.LocalId);
            Assert.Null(localizacao.Pilha);
            Assert.Equal(Guid.Empty, localizacao.PilhaId);

            filialRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
            armazemRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
            localRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            pilhaRepositoryMock.Verify(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }


        [Fact(DisplayName = "ObterLocalizacao")]
        [Trait("Category", "LocalizacaoDto Scopes")]
        void
            LocalizacaoDto_SemArmazemQueNaoAcheFilialLocal_ObterLocalizacao_DeveRetornar_LocalizacaoValueObject_ComFilialLocal_Vazios()
        {
            //Arrange
            var filialRepositoryMock = new Mock<IFilialRepository>();
            var armazemRepositoryMock = new Mock<IArmazemRepository>();
            var localRepositoryMock = new Mock<ILocalRepository>();
            var pilhaRepositoryMock = new Mock<IPilhaRepository>();

            filialRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => null);

            armazemRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => null);

            localRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => null);

            pilhaRepositoryMock.Setup(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => null);

            var localizacaoDto = new LocalizacaoDto()
            {
                ArmazemCodigo = string.Empty
            };

            //Act
            var localizacao = localizacaoDto.ObterLocalizacao(filialRepositoryMock.Object, armazemRepositoryMock.Object,
                localRepositoryMock.Object, pilhaRepositoryMock.Object);

            //Assert
            Assert.Equal(Guid.Empty, localizacao.FilialId);
            Assert.Null(localizacao.Armazem);
            Assert.Equal(Guid.Empty, localizacao.ArmazemId);
            Assert.Null(localizacao.Local);
            Assert.Equal(Guid.Empty, localizacao.LocalId);
            Assert.Null(localizacao.Pilha);
            Assert.Equal(Guid.Empty, localizacao.PilhaId);

            filialRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
            armazemRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            localRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
            pilhaRepositoryMock.Verify(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }


        [Fact(DisplayName = "ObterLocalizacao")]
        [Trait("Category", "LocalizacaoDto Scopes")]
        void
            LocalizacaoDto_ComArmazemQueAcheFilialArmazemPilha_ObterLocalizacao_DeveRetornar_LocalizacaoValueObject_ComFilialArmazemPilha_Preenchidos()
        {
            //Arrange
            var filialId = Guid.NewGuid();
            var armazemId = Guid.NewGuid();
            var pilhaId = Guid.NewGuid();

            var filialRepositoryMock = new Mock<IFilialRepository>();
            var armazemRepositoryMock = new Mock<IArmazemRepository>();
            var localRepositoryMock = new Mock<ILocalRepository>();
            var pilhaRepositoryMock = new Mock<IPilhaRepository>();

            filialRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => new FilialBuilder()
                    .ComFilialId(filialId));

            armazemRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => new ArmazemBuilder()
                    .ComArmazemId(armazemId));

            pilhaRepositoryMock.Setup(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => new PilhaBuilder()
                    .ComPilhaId(pilhaId));

            var localizacaoDto = new LocalizacaoDto()
            {
                ArmazemCodigo = "35B"
            };

            //Act
            var localizacao = localizacaoDto.ObterLocalizacao(filialRepositoryMock.Object, armazemRepositoryMock.Object,
                localRepositoryMock.Object, pilhaRepositoryMock.Object);

            //Assert
            Assert.Equal(filialId, localizacao.FilialId);
            Assert.NotNull(localizacao.Armazem);
            Assert.Equal(armazemId, localizacao.ArmazemId);
            Assert.Null(localizacao.Local);
            Assert.Equal(Guid.Empty, localizacao.LocalId);
            Assert.NotNull(localizacao.Pilha);
            Assert.Equal(pilhaId, localizacao.PilhaId);

            filialRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
            armazemRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
            localRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            pilhaRepositoryMock.Verify(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact(DisplayName = "ObterLocalizacao")]
        [Trait("Category", "LocalizacaoDto Scopes")]
        void
            LocalizacaoDto_SemArmazemQueAcheFilialLocal_ObterLocalizacao_DeveRetornar_LocalizacaoValueObject_ComFilialLocal_Preenchidos()
        {
            //Arrange
            var filialId = Guid.NewGuid();
            var localId = Guid.NewGuid();

            var filialRepositoryMock = new Mock<IFilialRepository>();
            var armazemRepositoryMock = new Mock<IArmazemRepository>();
            var localRepositoryMock = new Mock<ILocalRepository>();
            var pilhaRepositoryMock = new Mock<IPilhaRepository>();

            filialRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(() => new FilialBuilder()
                    .ComFilialId(filialId));

            localRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(() => new LocalBuilder()
                    .ComLocalId(localId));

            var localizacaoDto = new LocalizacaoDto()
            {
                ArmazemCodigo = string.Empty
            };

            //Act
            var localizacao = localizacaoDto.ObterLocalizacao(filialRepositoryMock.Object, armazemRepositoryMock.Object,
                localRepositoryMock.Object, pilhaRepositoryMock.Object);

            //Assert
            Assert.Equal(filialId, localizacao.FilialId);
            Assert.Null(localizacao.Armazem);
            Assert.Equal(Guid.Empty, localizacao.ArmazemId);
            Assert.NotNull(localizacao.Local);
            Assert.Equal(localId, localizacao.LocalId);
            Assert.Null(localizacao.Pilha);
            Assert.Equal(Guid.Empty, localizacao.PilhaId);

            filialRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
            armazemRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            localRepositoryMock.Verify(x => x.ObterPorCodigo(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
            pilhaRepositoryMock.Verify(x => x.ObterPorCodigoArmazemQuadraBloco(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }
    }
}