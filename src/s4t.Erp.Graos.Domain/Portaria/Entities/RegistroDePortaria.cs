using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Core.Domain.ValueObjects.Placa;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using s4t.Erp.Graos.Domain.Portaria.Enums;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Portaria.Entities
{
    public class RegistroDePortaria : Entity
    {
        private readonly IList<NotaFiscalGraos> _notasFiscais;

        public RegistroDePortaria(Guid id, Guid filialId, TipoGrao tipoGrao, TipoOperacaoPortaria tipoOperacaoPortaria, Placa placa,
            Motorista motorista, DateTime data)
        {
            Id = id;
            FilialId = filialId;
            TipoGrao = tipoGrao;
            TipoOperacaoPortaria = tipoOperacaoPortaria;
            Placa = placa;
            Motorista = motorista;
            Data = data;

            _notasFiscais = new List<NotaFiscalGraos>();
        }

        // Empty constructor for EF
        protected RegistroDePortaria()
        {
        }

        public Guid FilialId { get; private set; }
        public TipoGrao TipoGrao { get; private set; }
        public TipoOperacaoPortaria TipoOperacaoPortaria { get; private set; }
        public Placa Placa { get; private set; }
        public Motorista Motorista { get; private set; }
        public DateTime Data { get; private set; }

        public ICollection<NotaFiscalGraos> NotasFiscais
        {
            get { return _notasFiscais; }
        }

        public void AdicionaNotaFiscal(NotaFiscalGraos notaFiscal)
        {
            _notasFiscais.Add(notaFiscal);
        }

    }
}