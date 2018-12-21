using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs
{
    public static class RegistraBoletimCommandScopes
    {
        public static bool PossuiFilialInformada(this RegistraBoletimCommand registraBoletimCommand, Filial filial)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(filial, "Filial não informada")
            );
        }

        public static bool PossuiUsuarioInformado(this RegistraBoletimCommand registraBoletimCommand, Usuario usuario)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(usuario, "Usuário não informado")
            );
        }

        public static bool PossuiNumeroInformado(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotEmpty(registraBoletimCommand.Numero, "Número do boletim não informado")
            );
        }

        public static bool PossuiDataInformada(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(registraBoletimCommand.Data != DateTime.MinValue, "Data do boletim não informada")
            );
        }

        public static bool PossuiItemInformado(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotEmpty(registraBoletimCommand.Item, "Número do item do boletim não informado")
            );
        }

        public static bool PossuiBoletimDocumentoInformado(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(registraBoletimCommand.BoletimDocumentoSerie != string.Empty && registraBoletimCommand.BoletimDocumentoNumero > 0,
                    "Documento do boletim não informado")
            );
        }

        public static bool PossuiLoteInformado(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotEmpty(registraBoletimCommand.LoteNumero, "Número do Lote não informado")
            );
        }

        public static bool PossuiSacasMaiorQueZero(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(registraBoletimCommand.Sacas > 0, "Quantidade de sacas deve ser maior que zero")
            );
        }

        public static bool PossuiLocalizacaoOrigemInformada(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(registraBoletimCommand.OrigemFilialCodigo > 0 && registraBoletimCommand.OrigemBloco != string.Empty,
                    "Localização de origem não informada")
            );
        }

        public static bool PossuiLocalizacaoDestinoInformada(this RegistraBoletimCommand registraBoletimCommand)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(registraBoletimCommand.DestinoFilialCodigo > 0 && registraBoletimCommand.DestinoBloco != string.Empty,
                    "Localização de destino não informada")
            );
        }

        //todo 27-02-18: remover PossuiLocalizacoesOrigemDestinoDistintas daqui, aproveitar testes 
        public static bool PossuiLocalizacoesOrigemDestinoDistintas(this RegistraBoletimCommand registraBoletimCommand, Localizacao localizacaoOrigem,
            Localizacao localizacaoDestino)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(localizacaoOrigem != localizacaoDestino, "Localizações de origem e destino devem ser diferentes")
            );
        }
    }
}