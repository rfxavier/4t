using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class OrdemCarregamentoBuilder
    {
        private Guid _ordemCarregamentoId = Guid.Empty;
        private Guid _filialId;
        private int _numero;
        private DateTime _data;

        public OrdemCarregamento Build()
        {
            return new OrdemCarregamento(_ordemCarregamentoId, _filialId, _numero, _data);
        }

        public OrdemCarregamentoBuilder ComOrdemCarregamentoId(Guid ordemCarregamentoId)
        {
            this._ordemCarregamentoId = ordemCarregamentoId;
            return this;
        }

        public OrdemCarregamentoBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public OrdemCarregamentoBuilder ComNumero(int numero)
        {
            this._numero = numero;
            return this;
        }

        public OrdemCarregamentoBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public static implicit operator OrdemCarregamento(OrdemCarregamentoBuilder instance)
        {
            return instance.Build();
        }

    }
}