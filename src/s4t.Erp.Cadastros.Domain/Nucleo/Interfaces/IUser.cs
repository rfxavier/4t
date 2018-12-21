using System.Collections.Generic;
using System.Security.Claims;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
