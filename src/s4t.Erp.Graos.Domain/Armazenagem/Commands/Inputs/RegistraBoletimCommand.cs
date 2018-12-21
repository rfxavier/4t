using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Commands.Inputs
{
    public abstract class RegistraBoletimCommand : ICommand
    {
        public Guid FilialId { get; protected set; }
        public Guid UsuarioId { get; protected set; }
        public string Numero { get; protected set; }
        public DateTime Data { get; protected set; }
        public string Item { get; protected set; }
        public int TipoGrao { get; protected set; }
        public string BoletimDocumentoSerie { get; protected set; }
        public int BoletimDocumentoNumero { get; protected set; }
        public string LoteNumero { get; protected set; }
        public int Sacas { get; protected set; }

        public int OrigemFilialCodigo { get; protected set; }
        public string OrigemArmazemCodigo { get; protected set; }
        public string OrigemQuadra { get; protected set; }
        public string OrigemBloco { get; protected set; }

        public int DestinoFilialCodigo { get; protected set; }
        public string DestinoArmazemCodigo { get; protected set; }
        public string DestinoQuadra { get; protected set; }
        public string DestinoBloco { get; protected set; }

        public string LoteUltimoNumero { get; protected set; }
        public int LoteUltimoSacas { get; protected set; }
    }
}