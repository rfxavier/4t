using s4t.Erp.Graos.Domain.Armazenagem.Entities;

namespace s4t.Erp.Graos.Domain.Armazenagem.Interfaces
{
    public interface IBoletimRepository
    {
        Boletim Add(Boletim boletim);
    }
}