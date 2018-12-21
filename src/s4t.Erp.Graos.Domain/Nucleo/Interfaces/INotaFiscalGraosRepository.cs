using System;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.Nucleo.Interfaces
{
    public interface INotaFiscalGraosRepository
    {
        NotaFiscalGraos GetById(Guid id);
        NotaFiscalGraos Add(NotaFiscalGraos notaFiscalGraos);
    }
}