using System;
using s4t.Erp.Graos.Domain.Portaria.Entities;

namespace s4t.Erp.Graos.Domain.Portaria.Interfaces
{
    public interface IProdutoPortariaRepository
    {
        ProdutoPortaria GetById(Guid id);
    }
}