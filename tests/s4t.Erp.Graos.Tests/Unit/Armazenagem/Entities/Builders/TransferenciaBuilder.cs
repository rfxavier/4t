using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class TransferenciaBuilder
    {
        private Guid _transferenciaId = Guid.Empty;
        private Guid _filialId;
        private int _numero;
        private DateTime _data;

        public Transferencia Build()
        {
            return new Transferencia(_transferenciaId, _filialId, _numero, _data);
        }

        public TransferenciaBuilder ComTransferenciaId(Guid transferenciaId)
        {
            this._transferenciaId = transferenciaId;
            return this;
        }

        public TransferenciaBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public TransferenciaBuilder ComNumero(int numero)
        {
            this._numero = numero;
            return this;
        }

        public TransferenciaBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public static implicit operator Transferencia(TransferenciaBuilder instance)
        {
            return instance.Build();
        }


    }
}