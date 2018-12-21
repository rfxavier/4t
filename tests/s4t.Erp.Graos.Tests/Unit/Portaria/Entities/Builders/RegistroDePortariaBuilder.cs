using s4t.Erp.Core.Domain.ValueObjects.Placa;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Entities.Builders
{
    public class RegistroDePortariaBuilder
    {
        private Guid _registroDePortariaId;
        private Guid _filialId;
        private TipoGrao _tipoGrao;
        private TipoOperacaoPortaria _tipoOperacaoPortaria;
        private Placa _placa;
        private Motorista _motorista;
        private DateTime _data;

        private List<NotaFiscalGraos> _notasFiscais = new List<NotaFiscalGraos>();

        public RegistroDePortaria Build()
        {
            var registroDePortaria = new RegistroDePortaria(_registroDePortariaId, _filialId, _tipoGrao,
                _tipoOperacaoPortaria, _placa, _motorista, _data);

            foreach (var notaFiscalGraos in _notasFiscais)
            {
                registroDePortaria.AdicionaNotaFiscal(notaFiscalGraos);
            }

            return registroDePortaria;
        }

        public RegistroDePortariaBuilder ComRegistroDePortariaId(Guid registroDePortariaId)
        {
            this._registroDePortariaId = registroDePortariaId;
            return this;
        }

        public RegistroDePortariaBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public RegistroDePortariaBuilder ComTipoGrao(TipoGrao tipoGrao)
        {
            this._tipoGrao = tipoGrao;
            return this;
        }

        public RegistroDePortariaBuilder ComTipoOperacaoPortaria(TipoOperacaoPortaria tipoOperacaoPortaria)
        {
            this._tipoOperacaoPortaria = tipoOperacaoPortaria;
            return this;
        }

        public RegistroDePortariaBuilder ComPlaca(Placa placa)
        {
            this._placa = placa;
            return this;
        }

        public RegistroDePortariaBuilder ComMotorista(Motorista motorista)
        {
            this._motorista = motorista;
            return this;
        }

        public RegistroDePortariaBuilder ComData(DateTime data)
        {
            this._data = data;
            return this;
        }

        public RegistroDePortariaBuilder ComNotasFiscais(List<NotaFiscalGraos> notasFiscais)
        {
            this._notasFiscais = notasFiscais;
            return this;
        }

        public static implicit operator RegistroDePortaria(
            RegistroDePortariaBuilder instance)
        {
            return instance.Build();
        }
    }
}