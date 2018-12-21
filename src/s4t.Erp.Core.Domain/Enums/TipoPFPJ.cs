using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.Enums
{
    public class TipoPFPJ: IEnumeration, IEquatable<TipoPFPJ>
    {
        public static TipoPFPJ PF { get; } = new TipoPFPJ("PF", "Pessoa Física");
        public static TipoPFPJ PJ { get; } = new TipoPFPJ("PJ", "Pessoa Jurídica");

        private TipoPFPJ(string val, string name)
        {
            Value = val;
            Name = name;
        }

        private TipoPFPJ()
        {
            // required for EF
        }

        public string Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<TipoPFPJ> List()
        {
            return new[] { PF, PJ };
        }

        public static TipoPFPJ FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static TipoPFPJ FromValue(string value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TipoPFPJ);
        }

        public bool Equals(TipoPFPJ other)
        {
            return other != null &&
                   Value == other.Value &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1670801664;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(TipoPFPJ pFPJ1, TipoPFPJ pFPJ2)
        {
            return EqualityComparer<TipoPFPJ>.Default.Equals(pFPJ1, pFPJ2);
        }

        public static bool operator !=(TipoPFPJ pFPJ1, TipoPFPJ pFPJ2)
        {
            return !(pFPJ1 == pFPJ2);
        }
    }
}