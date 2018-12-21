using System;
using System.Collections.Generic;
using System.Linq;
using s4t.Erp.Core.Domain.Models;

namespace s4t.Erp.Cadastros.Domain.Graos.Fazendas.Enums
{
    public class FazendaCertificacaoTipoCultura : IEnumeration, IEquatable<FazendaCertificacaoTipoCultura>
    {
        public static FazendaCertificacaoTipoCultura Cafe { get; } =
            new FazendaCertificacaoTipoCultura(1, "Café");

        public static FazendaCertificacaoTipoCultura Milho { get; } =
            new FazendaCertificacaoTipoCultura(2, "Milho");

        public static FazendaCertificacaoTipoCultura Soja { get; } =
            new FazendaCertificacaoTipoCultura(3, "Soja");

        private FazendaCertificacaoTipoCultura(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private FazendaCertificacaoTipoCultura()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<FazendaCertificacaoTipoCultura> List()
        {
            return new[]
            {
                Cafe,
                Milho,
                Soja
            };
        }

        public static FazendaCertificacaoTipoCultura FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static FazendaCertificacaoTipoCultura FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FazendaCertificacaoTipoCultura);
        }

        public bool Equals(FazendaCertificacaoTipoCultura other)
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

        public static bool operator ==(FazendaCertificacaoTipoCultura cultura1, FazendaCertificacaoTipoCultura cultura2)
        {
            return EqualityComparer<FazendaCertificacaoTipoCultura>.Default.Equals(cultura1, cultura2);
        }

        public static bool operator !=(FazendaCertificacaoTipoCultura cultura1, FazendaCertificacaoTipoCultura cultura2)
        {
            return !(cultura1 == cultura2);
        }
    }
}
