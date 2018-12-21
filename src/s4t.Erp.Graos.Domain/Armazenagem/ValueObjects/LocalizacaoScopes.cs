using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.ValueObjects
{
    public static class LocalizacaoScopes
    {
        public static bool EstaValidaComArmazem(this Localizacao localizacao)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertGuidIsNotEmpty(localizacao.FilialId, "Filial da pilha não é válida")
                //AssertionConcern.AssertGuidIsNotEmpty(localizacao.ArmazemId, "Armazém da pilha não é válido")
            );
        }

        public static bool EstaValidaSemArmazem(this Localizacao localizacao)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertGuidIsNotEmpty(localizacao.FilialId, "Filial da localização não é válida"),
                AssertionConcern.AssertGuidIsNotEmpty(localizacao.LocalId, "Local temporário não é válido")
            );
        }

        public static bool EstaValida(this Localizacao localizacao)
        {
            return localizacao.Tipo() == LocalizacaoTipo.Pilha && localizacao.EstaValidaComArmazem() ||
                   localizacao.Tipo() == LocalizacaoTipo.Local && localizacao.EstaValidaSemArmazem();
        }

        public static bool PossuiPilhaCadastrada(this Localizacao localizacao)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(localizacao.Pilha != null && localizacao.Pilha.Id != Guid.Empty, "Pilha não cadastrada")
                );
        }

        public static bool PossuiLocalCadastrado(this Localizacao localizacao)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(localizacao.Local != null && localizacao.Local.Id != Guid.Empty, "Local não cadastrado")
            );
        }

        public static bool EstaCadastrada(this Localizacao localizacao)
        {
            return localizacao.Tipo() == LocalizacaoTipo.Pilha && localizacao.PossuiPilhaCadastrada() ||
                   localizacao.Tipo() == LocalizacaoTipo.Local && localizacao.PossuiLocalCadastrado();
        }

        public static bool EstaValidaECadastrada(this Localizacao localizacao)
        {
            return localizacao.EstaValida() && localizacao.EstaCadastrada();
        }
    }
}