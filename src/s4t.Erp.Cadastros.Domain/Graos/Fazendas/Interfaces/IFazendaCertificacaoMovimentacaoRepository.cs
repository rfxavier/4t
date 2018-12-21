using System;
using System.Collections.Generic;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;

namespace s4t.Erp.Cadastros.Domain.Graos.Fazendas.Interfaces
{
    public interface IFazendaCertificacaoMovimentacaoRepository
    {
        IEnumerable<FazendaCertificacao> GetCertificacoesEmVigencia(Guid fazendaId, int certificacaoTipoCultura, DateTime data);
    }
}