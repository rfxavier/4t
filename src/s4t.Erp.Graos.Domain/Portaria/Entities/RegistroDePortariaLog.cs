using s4t.Erp.Core.Domain.Models;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Entities
{
    public class RegistroDePortariaLog : Entity
    {
        public RegistroDePortariaLog()
        {
            Id = Guid.NewGuid();
        }

        public Guid RegistroDePortariaId { get; set; }
        public Guid RegistroDePortariaServicoAvulsoId { get; set; }
        public int FilialCodigo { get; set; }
        public string FilialNome { get; set; }
        public int TipoGrao { get; set; }
        public string TipoGraoDescricao { get; set; }
        public int TipoOperacao { get; set; }
        public string TipoOperacaoDescricao { get; set; }
        public string Placa { get; set; }
        public int MotoristaCodigo { get; set; }
        public string MotoristaNome { get; set; }
        public string NomeDoMotoristaSemCadastro { get; set; }
        public int ProdutoPortariaCodigo { get; set; }
        public string ProdutoPortariaDescricao { get; set; }
        public DateTime Data { get; set; }
    }
}