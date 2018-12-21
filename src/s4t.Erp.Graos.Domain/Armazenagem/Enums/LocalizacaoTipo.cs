using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Armazenagem.Enums
{
    public class LocalizacaoTipo : IEnumeration, IEquatable<LocalizacaoTipo>
    {
        public static LocalizacaoTipo Pilha { get; } = new LocalizacaoTipo(1, "Pilha");
        public static LocalizacaoTipo Local { get; } = new LocalizacaoTipo(2, "Local");

        private LocalizacaoTipo(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private LocalizacaoTipo()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<LocalizacaoTipo> List()
        {
            return new[] { Pilha, Local };
        }

        public static LocalizacaoTipo FromString(string localizacaoTipoString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, localizacaoTipoString, StringComparison.OrdinalIgnoreCase));
        }

        public static LocalizacaoTipo FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LocalizacaoTipo);
        }

        public bool Equals(LocalizacaoTipo other)
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

        public static bool operator ==(LocalizacaoTipo tipo1, LocalizacaoTipo tipo2)
        {
            return EqualityComparer<LocalizacaoTipo>.Default.Equals(tipo1, tipo2);
        }

        public static bool operator !=(LocalizacaoTipo tipo1, LocalizacaoTipo tipo2)
        {
            return !(tipo1 == tipo2);
        }
    }
}