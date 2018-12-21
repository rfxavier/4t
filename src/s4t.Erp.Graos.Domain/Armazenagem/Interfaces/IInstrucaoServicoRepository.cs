using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface IInstrucaoServicoRepository
    {
        InstrucaoServico ObterPorNumero(Guid filialId, int numero);
    }
}