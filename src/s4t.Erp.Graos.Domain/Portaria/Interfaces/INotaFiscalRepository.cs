using System;
using System.Collections.Generic;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.Portaria.Interfaces
{
    public interface INotaFiscalRepository
    {
        NotaFiscalGraos GetById(Guid id);
        IEnumerable<NotaFiscalGraos> GetByListaDeIds(IEnumerable<Guid> ids);
    }
}