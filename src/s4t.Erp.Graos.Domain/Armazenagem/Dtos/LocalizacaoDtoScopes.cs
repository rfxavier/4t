using s4t.Erp.Cadastros.Domain.Nucleo.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Interfaces;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Dtos
{
    public static class LocalizacaoDtoScopes
    {
        public static Localizacao ObterLocalizacao(this LocalizacaoDto localizacaoDto, IFilialRepository filialRepository,
            IArmazemRepository armazemRepository, ILocalRepository localRepository, IPilhaRepository pilhaRepository)
        {
            var filial = filialRepository.ObterPorCodigo(localizacaoDto.EmpresaId, localizacaoDto.FilialCodigo);

            var filialId = filial == null ? Guid.Empty : filial.Id;

            Armazem armazem = null;
            if (localizacaoDto.ArmazemCodigo != string.Empty) armazem = armazemRepository.ObterPorCodigo(filialId, localizacaoDto.ArmazemCodigo);

            Local local = null;
            if (localizacaoDto.ArmazemCodigo == string.Empty) local = localRepository.ObterPorCodigo(filialId, localizacaoDto.Bloco);

            Pilha pilha = null;
            if (localizacaoDto.ArmazemCodigo != string.Empty) pilha = pilhaRepository.ObterPorCodigoArmazemQuadraBloco(filialId, localizacaoDto.ArmazemCodigo, localizacaoDto.Quadra, localizacaoDto.Bloco);

            return new Localizacao(filialId, armazem, localizacaoDto.Quadra, localizacaoDto.Bloco, local, pilha);
        }
    }
}