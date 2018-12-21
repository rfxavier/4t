using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Nucleo.Enums
{
    public class TipoOperacao : IEquatable<TipoOperacao>
    {
        public static TipoOperacao DepositoComercializacao { get; } = new TipoOperacao(1, "Depósito para Comercialização");

        private TipoOperacao(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private TipoOperacao()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<TipoOperacao> List()
        {
            return new[] { DepositoComercializacao };
        }

        public static TipoOperacao FromString(string tipoOperacaoString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, tipoOperacaoString, StringComparison.OrdinalIgnoreCase));
        }

        public static TipoOperacao FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TipoOperacao);
        }

        public bool Equals(TipoOperacao other)
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

        public static bool operator ==(TipoOperacao operacao1, TipoOperacao operacao2)
        {
            return EqualityComparer<TipoOperacao>.Default.Equals(operacao1, operacao2);
        }

        public static bool operator !=(TipoOperacao operacao1, TipoOperacao operacao2)
        {
            return !(operacao1 == operacao2);
        }
    }
}