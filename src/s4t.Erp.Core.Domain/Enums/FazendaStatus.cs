using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Core.Domain.Enums
{
    public class FazendaStatus : IEnumeration, IEquatable<FazendaStatus>
    {
        public static FazendaStatus Ativa { get; } = new FazendaStatus(1, "Ativa");
        public static FazendaStatus Inativa { get; } = new FazendaStatus(2, "Inativa");

        private FazendaStatus(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private FazendaStatus()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<FazendaStatus> List()
        {
            return new[] { Ativa, Inativa };
        }

        public static FazendaStatus FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static FazendaStatus FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FazendaStatus);
        }

        public bool Equals(FazendaStatus other)
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

        public static bool operator ==(FazendaStatus status1, FazendaStatus status2)
        {
            return EqualityComparer<FazendaStatus>.Default.Equals(status1, status2);
        }

        public static bool operator !=(FazendaStatus status1, FazendaStatus status2)
        {
            return !(status1 == status2);
        }
    }
}