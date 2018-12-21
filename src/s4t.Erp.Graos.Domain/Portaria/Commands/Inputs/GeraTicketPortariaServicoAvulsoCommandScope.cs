using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.DomainNotification;
using s4t.Erp.Graos.Domain.Portaria.Entities;

namespace s4t.Erp.Graos.Domain.Portaria.Commands.Inputs
{
    public static class GeraTicketPortariaServicoAvulsoCommandScope
    {
        public static bool PossuiFilialInformada(this GeraTicketPortariaServicoAvulsoCommand geraTicketPortariaCommand, Filial filial)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(filial, "Filial não informada")
            );
        }

        public static bool PossuiProdutoPortariaInformado(this GeraTicketPortariaServicoAvulsoCommand geraTicketPortariaCommand,
            ProdutoPortaria produtoPortaria)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotNull(produtoPortaria, "Produto Serviço Avulso não informado")
            );
        }
    }
}