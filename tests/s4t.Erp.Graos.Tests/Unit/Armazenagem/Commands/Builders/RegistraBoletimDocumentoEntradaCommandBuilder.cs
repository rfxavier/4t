using s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Commands.Builders
{
    public class RegistraBoletimDocumentoEntradaCommandBuilder
    {
        private Guid _filialId = Guid.Empty;
        private Guid _usuarioId = Guid.Empty;
        private string _numero;
        private DateTime _data;
        private string _item;
        private int _tipoGrao;
        private int _boletimDocumentoNumero;
        private string _loteNumero = String.Empty;
        private int _sacas = 0;
        private int _origemFilialCodigo = 0;
        private string _origemArmazemCodigo = String.Empty;
        private string _origemQuadra = String.Empty;
        private string _origemBloco = String.Empty;
        private int _destinoFilialCodigo = 0;
        private string _destinoArmazemCodigo = String.Empty;
        private string _destinoQuadra = String.Empty;
        private string _destinoBloco = String.Empty;
        private string _loteUltimoNumero = String.Empty;
        private int _loteUltimoSacas = 0;

        public RegistraBoletimDocumentoEntradaCommand Build()
        {
            return new RegistraBoletimDocumentoEntradaCommand(_filialId, _usuarioId, _numero, _data, _item, _tipoGrao, _boletimDocumentoNumero, _loteNumero, _sacas,
                _origemFilialCodigo, _origemArmazemCodigo, _origemQuadra, _origemBloco, _destinoFilialCodigo, _destinoArmazemCodigo, _destinoQuadra,
                _destinoBloco, _loteUltimoNumero, _loteUltimoSacas);
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComUsuarioId(Guid usuarioId)
        {
            this._usuarioId = usuarioId;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComNumero(string numero)
        {
            this._numero = numero;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComItem(string item)
        {
            this._item = item;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComTipoGrao(int tipoGrao)
        {
            this._tipoGrao = tipoGrao;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComBoletimDocumentoNumero(int boletimDocumentoNumero)
        {
            this._boletimDocumentoNumero = boletimDocumentoNumero;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComLoteNumero(string loteNumero)
        {
            this._loteNumero = loteNumero;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComSacas(int sacas)
        {
            this._sacas = sacas;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComOrigemFilialCodigo(int origemFilialCodigo)
        {
            this._origemFilialCodigo = origemFilialCodigo;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComOrigemArmazemCodigo(string origemArmazemCodigo)
        {
            this._origemArmazemCodigo = origemArmazemCodigo;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComOrigemQuadra(string origemQuadra)
        {
            this._origemQuadra = origemQuadra;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComOrigemBloco(string origemBloco)
        {
            this._origemBloco = origemBloco;
            return this;
        }
        public RegistraBoletimDocumentoEntradaCommandBuilder ComDestinoFilialCodigo(int destinoFilialCodigo)
        {
            this._destinoFilialCodigo = destinoFilialCodigo;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComDestinoArmazemCodigo(string destinoArmazemCodigo)
        {
            this._destinoArmazemCodigo = destinoArmazemCodigo;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComDestinoQuadra(string destinoQuadra)
        {
            this._destinoQuadra = destinoQuadra;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComDestinoBloco(string destinoBloco)
        {
            this._destinoBloco = destinoBloco;
            return this;
        }

        public RegistraBoletimDocumentoEntradaCommandBuilder ComLoteUltimoNumero(string loteUltimoNumero)
        {
            this._loteUltimoNumero = loteUltimoNumero;
            return this;
        }
        public RegistraBoletimDocumentoEntradaCommandBuilder ComLoteUltimoSacas(int loteUltimoSacas)
        {
            this._loteUltimoSacas = loteUltimoSacas;
            return this;
        }

        public static implicit operator RegistraBoletimDocumentoEntradaCommand(
            RegistraBoletimDocumentoEntradaCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}