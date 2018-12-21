using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace s4t.Erp.Graos.Domain.Portaria.Enums
{
    public class TipoServicoPortaria : IEnumeration, IEquatable<TipoServicoPortaria>
    {
        public static TipoServicoPortaria Normal { get; } =
            new TipoServicoPortaria(1, "Normal");

        public static TipoServicoPortaria Avulso { get; } =
            new TipoServicoPortaria(2, "Avulso");

        private TipoServicoPortaria(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private TipoServicoPortaria()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<TipoServicoPortaria> List()
        {
            return new[]
            {
                Normal,
                Avulso
            };
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TipoServicoPortaria);
        }

        public bool Equals(TipoServicoPortaria other)
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

        public static bool operator ==(TipoServicoPortaria portaria1, TipoServicoPortaria portaria2)
        {
            return EqualityComparer<TipoServicoPortaria>.Default.Equals(portaria1, portaria2);
        }

        public static bool operator !=(TipoServicoPortaria portaria1, TipoServicoPortaria portaria2)
        {
            return !(portaria1 == portaria2);
        }
    }
}