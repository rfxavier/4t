using s4t.Erp.Core.Domain.DomainNotification;
using System.Text.RegularExpressions;

namespace s4t.Erp.Core.Domain.ValueObjects.Placa
{
    public static class PlacaScopes
    {
        public static bool EstaValida(this Placa placa)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(string.IsNullOrEmpty(placa.Numero) || PlacaValida(placa.Numero), "Placa de veículo deve ser válida")
                );
        }

        public static bool EstaPreenchida(this Placa placa)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertNotEmpty(placa.Numero, "Placa de veículo é obrigatória")
                );
        }

        public static bool EstaValidaEPreenchida(this Placa placa)
        {
            return EstaPreenchida(placa) && EstaValida(placa);
        }

        //@"^[a-zA-Z]{3}\-\d{4}$");
        private static bool PlacaValida(string arg)
        {
            Regex regex = new Regex(@"^[a-zA-Z]{3}\d{4}$");

            if (regex.IsMatch(arg))
            {
                return true;
            }

            return false;
        }

    }
}
