using s4t.Erp.Graos.Data.Context;
using s4t.Erp.Graos.Data.Repository;
using s4t.Erp.Graos.Data.UoW;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using s4t.Erp.Graos.Tests.Unit.RecepcaoExpedicao.Entities.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Xunit;

namespace s4t.Erp.Graos.Tests.Integration
{
    public class DocumentoEntradaTests
    {
        [Fact]
        [Trait("Category", "Integration DocumentoEntrada")]
        public void DocumentoEntrada_ComNotaFiscalGraos_InsertTests()
        {

            using (var context = new GraosContext())
            {
                using (var uow = new UnitOfWork(context))
                {
                    var repoNotaFiscalGraos = new NotaFiscalGraosRepository(context);

                    var notaFiscalGraos = new NotaFiscalGraos()
                    {
                        TipoGrao = 1
                    };

                    repoNotaFiscalGraos.Add(notaFiscalGraos);

                    Guid notaFiscaoGraosId = notaFiscalGraos.Id;

                    var repoDocumentoEntrada = new DocumentoEntradaRepository(context);

                    var documentoEntradaEmbalagens = new List<DocumentoEntradaEmbalagem>
                    {
                        new DocumentoEntradaEmbalagem(Guid.NewGuid(), new Embalagem(Guid.NewGuid(), 1, "Embalagem 1", 60, 0.5), 10),
                        new DocumentoEntradaEmbalagem(Guid.NewGuid(), new Embalagem(Guid.NewGuid(), 2, "Embalagem 2", 60, 0.5), 15)
                    };

                    var documentoEntrada = new DocumentoEntradaBuilder()
                        .ComDocumentoEntradaId(Guid.NewGuid())
                        .ComTipoOperacao(TipoOperacao.DepositoComercializacao)
                        .ComData(DateTime.Now)
                        .ComNotaFiscalGraos(notaFiscalGraos)
                        .ComEmbalagens(documentoEntradaEmbalagens);

                    DocumentoEntrada docEntradaParaAdd = documentoEntrada;

                    Guid documentoEntradaIdAntesDoAdd = docEntradaParaAdd.Id;

                    DocumentoEntrada docEntradaDepoisDoAdd = repoDocumentoEntrada.Add(docEntradaParaAdd);

                    Guid documentoEntradaIdDepoisDoAdd = docEntradaDepoisDoAdd.Id;

                    uow.Commit();

                    DocumentoEntrada docEntradaDepoisDoAddCommitPeloIdDocEntradaAntesDoAdd = repoDocumentoEntrada.ObterPorId(documentoEntradaIdAntesDoAdd);
                    DocumentoEntrada docEntradaDepoisDoAddCommitPeloIdNotaFiscalGraos = repoDocumentoEntrada.ObterPorId(notaFiscaoGraosId);

                    Assert.Equal(documentoEntradaIdAntesDoAdd, documentoEntradaIdDepoisDoAdd);
                    Assert.Null(docEntradaDepoisDoAddCommitPeloIdDocEntradaAntesDoAdd);
                    Assert.NotNull(docEntradaDepoisDoAddCommitPeloIdNotaFiscalGraos);
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration DocumentoEntrada")]
        public void DadoUm_DocumentoEntrada_SemNotaFiscalGraos_DeveDarErroEmInsert()
        {
            Exception ex;
            using (var context = new GraosContext())
            {
                using (var uow = new UnitOfWork(context))
                {
                    var repoDocumentoEntrada = new DocumentoEntradaRepository(context);

                    var documentoEntrada = new DocumentoEntradaBuilder()
                        .ComDocumentoEntradaId(Guid.NewGuid())
                        .ComTipoOperacao(TipoOperacao.DepositoComercializacao)
                        .ComData(DateTime.Now)
                        .ComNotaFiscalGraos(null);

                    repoDocumentoEntrada.Add(documentoEntrada);

                    ex = Assert.Throws<DbUpdateException>(() => uow.Commit());
                }
            }

            Assert.Equal("An error occurred while updating the entries. See the inner exception for details.", ex.Message);
        }
    }
}