using s4t.Erp.Graos.Domain.Portaria.Commands.Inputs;
using System;

namespace s4t.Erp.Graos.Tests.Unit.Portaria.Commands.Builders
{
    public class GeraTicketPortariaServicoAvulsoCommandBuilder
    {
        private Guid _filialId = Guid.Empty;
        private string _placaNumero = String.Empty;
        private Guid _motoristaId = Guid.Empty;
        private string _nomeDoMotoristaSemCadastro = String.Empty;
        private Guid _produtoPortariaId = Guid.Empty;

        public GeraTicketPortariaServicoAvulsoCommand Build()
        {
            return new GeraTicketPortariaServicoAvulsoCommand(_filialId, _placaNumero, _motoristaId,
                _nomeDoMotoristaSemCadastro,
                _produtoPortariaId);
        }

        public GeraTicketPortariaServicoAvulsoCommandBuilder ComFilialId(Guid filialId)
        {
            this._filialId = filialId;
            return this;
        }

        public GeraTicketPortariaServicoAvulsoCommandBuilder ComPlacaNumero(string placaNumero)
        {
            this._placaNumero = placaNumero;
            return this;
        }

        public GeraTicketPortariaServicoAvulsoCommandBuilder ComMotoristaId(Guid motoristaId)
        {
            this._motoristaId = motoristaId;
            return this;
        }

        public GeraTicketPortariaServicoAvulsoCommandBuilder ComNomeDoMotoristaSemCadastro(string nomeDoMotoristaSemCadastro)
        {
            this._nomeDoMotoristaSemCadastro = nomeDoMotoristaSemCadastro;
            return this;
        }

        public GeraTicketPortariaServicoAvulsoCommandBuilder ComProdutoPortariaId(Guid produtoPortariaId)
        {
            this._produtoPortariaId = produtoPortariaId;
            return this;
        }

        public static implicit operator GeraTicketPortariaServicoAvulsoCommand(GeraTicketPortariaServicoAvulsoCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}