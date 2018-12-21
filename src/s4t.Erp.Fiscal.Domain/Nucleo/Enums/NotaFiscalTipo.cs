using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Enums
{
    public class NotaFiscalTipo : IEnumeration, IEquatable<NotaFiscalTipo>
    {
        public static NotaFiscalTipo SaidaParaVenda { get; } =
            new NotaFiscalTipo(1, "Saída Para Venda");

        public static NotaFiscalTipo EntradaParaDeposito { get; } =
            new NotaFiscalTipo(2, "Entrada Para Depósito");

        public static NotaFiscalTipo RemessaParaDeposito { get; } =
            new NotaFiscalTipo(3, "Remessa Para Depósito");

        public static NotaFiscalTipo SaidaParaTransferencia { get; } =
            new NotaFiscalTipo(4, "Saída Para Transferência");

        private NotaFiscalTipo(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private NotaFiscalTipo()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<NotaFiscalTipo> List()
        {
            return new[]
            {
                SaidaParaVenda,
                EntradaParaDeposito,
                RemessaParaDeposito,
                SaidaParaTransferencia
            };
        }

        public static NotaFiscalTipo FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static NotaFiscalTipo FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NotaFiscalTipo);
        }

        public bool Equals(NotaFiscalTipo other)
        {
            return other != null &&
                   Value == other.Value &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1670801664;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(NotaFiscalTipo tipo1, NotaFiscalTipo tipo2)
        {
            return EqualityComparer<NotaFiscalTipo>.Default.Equals(tipo1, tipo2);
        }

        public static bool operator !=(NotaFiscalTipo tipo1, NotaFiscalTipo tipo2)
        {
            return !(tipo1 == tipo2);
        }
    }
}