using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Fiscal.Domain.Nucleo.Enums
{
    public class NotaFiscalModoInclusao : IEnumeration, IEquatable<NotaFiscalModoInclusao>
    {
        public static NotaFiscalModoInclusao Emissao { get; } =
            new NotaFiscalModoInclusao(1, "Emissão");

        public static NotaFiscalModoInclusao Digitacao { get; } =
            new NotaFiscalModoInclusao(2, "Digitação");


        private NotaFiscalModoInclusao(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private NotaFiscalModoInclusao()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<NotaFiscalModoInclusao> List()
        {
            return new[] { Emissao, Digitacao };
        }

        public static NotaFiscalModoInclusao FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static NotaFiscalModoInclusao FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NotaFiscalModoInclusao);
        }

        public bool Equals(NotaFiscalModoInclusao other)
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

        public static bool operator ==(NotaFiscalModoInclusao inclusao1, NotaFiscalModoInclusao inclusao2)
        {
            return EqualityComparer<NotaFiscalModoInclusao>.Default.Equals(inclusao1, inclusao2);
        }

        public static bool operator !=(NotaFiscalModoInclusao inclusao1, NotaFiscalModoInclusao inclusao2)
        {
            return !(inclusao1 == inclusao2);
        }
    }
}