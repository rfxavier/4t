using s4t.Erp.Core.Domain.Commands;

namespace s4t.Erp.Graos.Domain.Balanca.Commands.Results
{
    public class PesagemCommandResult : ICommandResult
    {
        public PesagemCommandResult(string numeroTicketPesagem)
        {
            NumeroTicketPesagem = numeroTicketPesagem;
        }
        public string NumeroTicketPesagem { get; set; }
    }
}