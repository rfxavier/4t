using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Nucleo.Enums
{
    public class TipoGrao : IEnumeration, IEquatable<TipoGrao>
    {
        public static TipoGrao Cafe { get; } = new TipoGrao(1, "Café");
        public static TipoGrao Milho { get; } = new TipoGrao(2, "Milho");
        public static TipoGrao Soja { get; } = new TipoGrao(3, "Soja");

        private TipoGrao(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private TipoGrao()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<TipoGrao> List()
        {
            return new[] { Cafe, Milho, Soja };
        }

        public static TipoGrao FromString(string tipoGraoString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, tipoGraoString, StringComparison.OrdinalIgnoreCase));
        }

        public static TipoGrao FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TipoGrao);
        }

        public bool Equals(TipoGrao other)
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

        public static bool operator ==(TipoGrao grao1, TipoGrao grao2)
        {
            return EqualityComparer<TipoGrao>.Default.Equals(grao1, grao2);
        }

        public static bool operator !=(TipoGrao grao1, TipoGrao grao2)
        {
            return !(grao1 == grao2);
        }
    }
}