using s4t.Erp.Core.Domain.Models;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Enums;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Entities
{
    public class Boletim : Entity
    {
        public Boletim(Guid id, Guid filialId, Guid usuarioId, string numero, DateTime data, string item, TipoGrao tipoGrao,
            BoletimDocumento boletimDocumento, Lote lote, int sacas, Localizacao origem, Localizacao destino,
            Guid loteUltimoId, int loteUltimoSacas, string es, string @is)
        {
            Id = id;
            FilialId = filialId;
            UsuarioId = usuarioId;
            Numero = numero;
            Data = data;
            Item = item;
            TipoGrao = tipoGrao;
            BoletimDocumento = boletimDocumento;
            Lote = lote;
            Sacas = sacas;
            Origem = origem;
            Destino = destino;
            LoteUltimoId = loteUltimoId;
            LoteUltimoSacas = loteUltimoSacas;
            ES = es;
            IS = @is;
        }


        public Guid FilialId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Numero { get; private set; }
        public DateTime Data { get; private set; }
        public string Item { get; private set; }
        public TipoGrao TipoGrao { get; private set; }
        public BoletimDocumento BoletimDocumento { get; private set; }
        public Guid LoteId { get; private set; }
        public Lote Lote { get; private set; }
        public int Sacas { get; private set; }
        public Localizacao Origem { get; private set; }
        public Localizacao Destino { get; private set; }
        public Guid LoteUltimoId { get; private set; }
        public int LoteUltimoSacas { get; private set; }
        public string ES { get; private set; }
        public string IS { get; private set; }
    }
}