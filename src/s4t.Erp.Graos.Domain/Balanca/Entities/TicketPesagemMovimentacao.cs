using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using System;

namespace s4t.Erp.Graos.Domain.Balanca.Entities
{
    public class TicketPesagemMovimentacao : Entity
    {
        public TicketPesagemMovimentacao(Guid id, double peso)
        {
            Id = id;
            Ordem = 0;
            Peso = peso;
            PesoDiferencialAnterior = 0;
            DataHora = DateTime.Now;
        }

        public Guid TicketPesagemId { get; private set; }
        public TicketPesagem TicketPesagem { get; private set; }
        public int Ordem { get; private set; }
        public double Peso { get; private set; }
        public double PesoDiferencialAnterior { get; private set; }
        public DateTime DataHora { get; private set; }
        public Lote Lote { get; private set; }
        public void MarcaOrdem(int ordem)
        {
            Ordem = ordem;
        }

        public void MarcaPesoDiferencialAnteriorComValorAbsolutoComUmaCasaDecimal(double pesoDiferencialAnterior)
        {
            PesoDiferencialAnterior = Math.Abs(Math.Round(pesoDiferencialAnterior, 1));
        }

        public void MarcaPesoComUmaCasaDecimal(double peso)
        {
            Peso = Math.Round(Peso, 1);
        }
    }
}