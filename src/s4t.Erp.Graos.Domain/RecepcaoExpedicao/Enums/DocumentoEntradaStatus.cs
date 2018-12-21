using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.RecepcaoExpedicao.Enums
{
    public class DocumentoEntradaStatus : IEnumeration, IEquatable<DocumentoEntradaStatus>
    {
        public static DocumentoEntradaStatus Aberto { get; } =
            new DocumentoEntradaStatus(1, "Aberto");

        public static DocumentoEntradaStatus ComplementadoComLotesEPesos { get; } =
            new DocumentoEntradaStatus(2, "Complementado Com Lotes e Pesos");

        //(1 -> 2) (2 -> 3) (0 -> 3) (3 -> 0) (3 ou 2 ou 1-> 0 sumir, cancelar)
        //todo (1 -> 2) (0 -> 2) (2 -> 0) (2 ou 1 ou 1-> 0 sumir, cancelar)

        private DocumentoEntradaStatus(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private DocumentoEntradaStatus()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<DocumentoEntradaStatus> List()
        {
            return new[]
            {
                Aberto,
                ComplementadoComLotesEPesos
            };
        }

        public static DocumentoEntradaStatus FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static DocumentoEntradaStatus FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DocumentoEntradaStatus);
        }

        public bool Equals(DocumentoEntradaStatus other)
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

        public static bool operator ==(DocumentoEntradaStatus status1, DocumentoEntradaStatus status2)
        {
            return EqualityComparer<DocumentoEntradaStatus>.Default.Equals(status1, status2);
        }

        public static bool operator !=(DocumentoEntradaStatus status1, DocumentoEntradaStatus status2)
        {
            return !(status1 == status2);
        }
    }
}