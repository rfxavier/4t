using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface IPilhaRepository
    {
        Pilha ObterPorCodigoArmazemQuadraBloco(Guid filialId, string localizacaoDtoArmazemCodigo, string localizacaoDtoQuadra, string localizacaoDtoBloco);
    }
}