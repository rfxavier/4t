using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Dtos
{
    public class BoletimDocumentoDto
    {
        public Guid FilialId { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
    }
}