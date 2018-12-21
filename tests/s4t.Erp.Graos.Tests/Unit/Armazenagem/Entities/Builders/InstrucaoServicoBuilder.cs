using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Armazenagem.Entities.Builders
{
    public class InstrucaoServicoBuilder
    {
        private Guid _instrucaoServicoId = Guid.Empty;
        private Guid _filialId;
        private int _numero;
        private DateTime _data;

        public InstrucaoServico Build()
        {
            return new InstrucaoServico(_instrucaoServicoId, _filialId, _numero, _data);
        }

        public InstrucaoServicoBuilder ComInstrucaoServicoId(Guid instrucaoServicoId)
        {
            this._instrucaoServicoId = instrucaoServicoId;
            return this;
        }

        public InstrucaoServicoBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public InstrucaoServicoBuilder ComNumero(int numero)
        {
            this._numero = numero;
            return this;
        }

        public InstrucaoServicoBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public static implicit operator InstrucaoServico(InstrucaoServicoBuilder instance)
        {
            return instance.Build();
        }

    }
}