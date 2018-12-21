using s4t.Erp.Cadastros.Data.Context;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;

namespace s4t.Erp.Cadastros.Data.Repository
{
    public class FazendaRepository : Repository<Fazenda>, IFazendaRepository
    {
        public FazendaRepository(CadastrosContext context) : base(context)
        {
        }

        public Fazenda Add(Fazenda fazenda)
        {
            return DbSet.Add(fazenda);
        }
    }
}
