using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Portaria.Enums
{
    public class NotaFiscalGraosTipo 
    {
        public static NotaFiscalGraosTipo EntradaParaDeposito { get; } = new NotaFiscalGraosTipo(1, "Entrada Para Depósito");
        public static NotaFiscalGraosTipo RemessaParaDeposito { get; } = new NotaFiscalGraosTipo(2, "Remessa Para Depósito");
        public static NotaFiscalGraosTipo SaidaParaTransferencia { get; } = new NotaFiscalGraosTipo(3, "Saída Para Transferência");

        private NotaFiscalGraosTipo(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private NotaFiscalGraosTipo()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<NotaFiscalGraosTipo> List()
        {
            return new[] { EntradaParaDeposito, RemessaParaDeposito, SaidaParaTransferencia };
        }

        public static NotaFiscalGraosTipo FromString(string notaFiscalGraosTipoString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, notaFiscalGraosTipoString, StringComparison.OrdinalIgnoreCase));
        }

        public static NotaFiscalGraosTipo FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NotaFiscalGraosTipo);
        }

        public bool Equals(NotaFiscalGraosTipo other)
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

        public static bool operator ==(NotaFiscalGraosTipo status1, NotaFiscalGraosTipo status2)
        {
            return EqualityComparer<NotaFiscalGraosTipo>.Default.Equals(status1, status2);
        }

        public static bool operator !=(NotaFiscalGraosTipo status1, NotaFiscalGraosTipo status2)
        {
            return !(status1 == status2);
        }
    }
}