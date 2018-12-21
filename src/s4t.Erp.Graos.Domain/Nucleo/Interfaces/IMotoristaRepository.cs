using System;
using s4t.Erp.Graos.Domain.Nucleo.Entities;

namespace s4t.Erp.Graos.Domain.Nucleo.Interfaces
{
    public interface IMotoristaRepository
    {
        Motorista GetById(Guid id);

    }
}