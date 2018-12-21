using s4t.Erp.Core.Domain.Commands;
using System;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public class GeraTicketPortariaServicoAvulsoCommand : ICommand
    {
        public GeraTicketPortariaServicoAvulsoCommand(Guid filialId, string placaNumero, Guid motoristaId,
            string nomeDoMotoristaSemCadastro, Guid produtoPortariaId)
        {
            FilialId = filialId;
            PlacaNumero = placaNumero;
            MotoristaId = motoristaId;
            NomeDoMotoristaSemCadastro = nomeDoMotoristaSemCadastro;
            ProdutoPortariaId = produtoPortariaId;
        }

        public Guid FilialId { get; private set; }
        public string PlacaNumero { get; set; }
        public Guid MotoristaId { get; private set; }
        public string NomeDoMotoristaSemCadastro { get; private set; }
        public Guid ProdutoPortariaId { get; private set; }
    }
}