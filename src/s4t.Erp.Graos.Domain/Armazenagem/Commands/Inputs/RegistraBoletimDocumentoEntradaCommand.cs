using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs
{
    public class RegistraBoletimDocumentoEntradaCommand : RegistraBoletimCommand
    {
        public RegistraBoletimDocumentoEntradaCommand(Guid filialId, Guid usuarioId, string numero, DateTime data, string item, int tipoGrao, 
            int boletimDocumentoNumero, string loteNumero, int sacas, int origemFilialCodigo, string origemArmazemCodigo, string origemQuadra,
            string origemBloco, int destinoFilialCodigo, string destinoArmazemCodigo, string destinoQuadra, string destinoBloco, string loteUltimoNumero,
            int loteUltimoSacas)
        {
            FilialId = filialId;
            UsuarioId = usuarioId;
            Numero = numero;
            Data = data;
            Item = item;
            TipoGrao = tipoGrao;
            BoletimDocumentoSerie = Enums.BoletimSerie.Nde.Value;
            BoletimDocumentoNumero = boletimDocumentoNumero;
            LoteNumero = loteNumero;
            Sacas = sacas;
            OrigemFilialCodigo = origemFilialCodigo;
            OrigemArmazemCodigo = origemArmazemCodigo;
            OrigemQuadra = origemQuadra;
            OrigemBloco = origemBloco;
            DestinoFilialCodigo = destinoFilialCodigo;
            DestinoArmazemCodigo = destinoArmazemCodigo;
            DestinoQuadra = destinoQuadra;
            DestinoBloco = destinoBloco;
            LoteUltimoNumero = loteUltimoNumero;
            LoteUltimoSacas = loteUltimoSacas;
        }
    }
}