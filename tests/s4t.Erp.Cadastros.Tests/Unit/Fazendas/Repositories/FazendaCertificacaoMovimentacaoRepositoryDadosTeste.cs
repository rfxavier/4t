using s4t.Erp.Cadastros.Tests.Unit.Nucleo.Entities.Builders;
using s4t.Erp.Core.Domain.ValueObjects.DateTimeRange;
using System;
using System.Collections;
using System.Collections.Generic;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums;

namespace s4t.Erp.Cadastros.Tests.Unit.Fazendas.Repositories
{
    public class FazendaCertificacaoMovimentacaoRepositoryDadosTeste : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                new List<FazendaCertificacao>()
                {
                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Milho)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31))),
                },
                Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B"), FazendaCertificacaoTipoCultura.Cafe,
                new DateTime(2018, 1, 9), 1
            },
            new object[]
            {
                new List<FazendaCertificacao>()
                {
                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Milho)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31))),
                },
                Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B"), FazendaCertificacaoTipoCultura.Cafe,
                new DateTime(2017, 6, 1), 2
            },
            new object[]
            {
                new List<FazendaCertificacao>()
                {
                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Milho)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31))),
                },
                Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78"), FazendaCertificacaoTipoCultura.Cafe,
                new DateTime(2018, 1, 9), 1
            },

            new object[]
            {
                new List<FazendaCertificacao>()
                {
                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Milho)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2018, 12, 31))),

                    new FazendaCertificacaoMovimentacaoBuilder()
                        .ComFazenda(new FazendaBuilder()
                            .ComFazendaId(Guid.Parse("B4256BD2-C42F-41D3-8354-0E43A55B612B")))
                        .ComFazendaCertificacaoTipoCultura(FazendaCertificacaoTipoCultura.Cafe)
                        .ComPeriodoVigencia(new DateTimeRange(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31))),
                },
                Guid.Parse("BB07993F-0BDA-4C11-8A66-06E483C08E78"), FazendaCertificacaoTipoCultura.Soja,
                new DateTime(2018, 1, 9), 0
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}