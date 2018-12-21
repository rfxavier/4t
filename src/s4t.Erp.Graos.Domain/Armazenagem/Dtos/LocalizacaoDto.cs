using System;

namespace s4t.Erp.Graos.Domain.Armazenagem.Dtos
{
    public class LocalizacaoDto
    {
        public Guid EmpresaId { get; set; }
        public int FilialCodigo { get; set; }
        public string ArmazemCodigo { get; set; }
        public string Quadra { get; set; }
        public string Bloco { get; set; }
    }
}