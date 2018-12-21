using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Cadastros.Domain.Nucleo.Entities
{
    public class Usuario : Entity
    {
        public Usuario(Guid id, string login, string senha, Cadastro cadastro, Guid usuarioAspNetIdentityId, bool ativo)
        {
            Id = id;
            Login = login;
            Senha = senha;
            Cadastro = cadastro;
            UsuarioAspNetIdentityId = usuarioAspNetIdentityId;
            Ativo = ativo;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public Cadastro Cadastro { get; private set; }
        public Guid UsuarioAspNetIdentityId { get; private set; }
        public bool Ativo { get; private set; }
    }
}