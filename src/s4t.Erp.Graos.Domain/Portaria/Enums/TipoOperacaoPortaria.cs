using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Portaria.Enums
{
    public class TipoOperacaoPortaria : IEnumeration, IEquatable<TipoOperacaoPortaria>
    {
        public static TipoOperacaoPortaria DesembarqueParaEntradaDeposito { get; } =
            new TipoOperacaoPortaria(1, "Desembarque Para Entrada Depósito");

        public static TipoOperacaoPortaria DesembarqueParaEntradaTransferencia { get; } =
            new TipoOperacaoPortaria(2, "Desembarque Para Entrada Transferência");

        public static TipoOperacaoPortaria EmbarqueParaSaidaRetirada { get; } =
            new TipoOperacaoPortaria(3, "Embarque Para Saída Retirada");

        public static TipoOperacaoPortaria EmbarqueParaSaidaTransferencia { get; } =
            new TipoOperacaoPortaria(4, "Embarque Para Saída Transferência");


        private TipoOperacaoPortaria(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private TipoOperacaoPortaria()
        {
            // required for EF
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<TipoOperacaoPortaria> List()
        {
            return new[]
            {
                DesembarqueParaEntradaDeposito,
                DesembarqueParaEntradaTransferencia,
                EmbarqueParaSaidaRetirada,
                EmbarqueParaSaidaTransferencia
            };
        }

        public static TipoOperacaoPortaria FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static TipoOperacaoPortaria FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TipoOperacaoPortaria);
        }

        public bool Equals(TipoOperacaoPortaria other)
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

        public static bool operator ==(TipoOperacaoPortaria portaria1, TipoOperacaoPortaria portaria2)
        {
            return EqualityComparer<TipoOperacaoPortaria>.Default.Equals(portaria1, portaria2);
        }

        public static bool operator !=(TipoOperacaoPortaria portaria1, TipoOperacaoPortaria portaria2)
        {
            return !(portaria1 == portaria2);
        }
    }
}