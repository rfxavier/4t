using s4t.Erp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace s4t.Erp.Graos.Domain.Armazenagem.Enums
{
    public class BoletimSerie : IEnumeration, IEquatable<BoletimSerie>
    {
        public static BoletimSerie Nde { get; } = new BoletimSerie("NDE", "Documento Entrada");
        public static BoletimSerie Is { get; } = new BoletimSerie("IS", "Instrução de Serviço");
        public static BoletimSerie Tis { get; } = new BoletimSerie("TIS", "Transferência Instrução de Serviço");
        public static BoletimSerie Tr { get; } = new BoletimSerie("TR", "Transferência");
        public static BoletimSerie Re { get; } = new BoletimSerie("RE", "Remoção");
        public static BoletimSerie Oc { get; } = new BoletimSerie("OC", "Ordem Carregamento");

        private BoletimSerie(string val, string name)
        {
            Value = val;
            Name = name;
        }

        private BoletimSerie()
        {
            // required for EF
        }

        public string Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<BoletimSerie> List()
        {
            return new[] { Nde, Is, Tis, Tr, Re, Oc };
        }

        public static BoletimSerie FromString(string boletimSerieString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, boletimSerieString, StringComparison.OrdinalIgnoreCase));
        }

        public static BoletimSerie FromValue(string value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BoletimSerie);
        }

        public bool Equals(BoletimSerie other)
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

        public static bool operator ==(BoletimSerie serie1, BoletimSerie serie2)
        {
            return EqualityComparer<BoletimSerie>.Default.Equals(serie1, serie2);
        }

        public static bool operator !=(BoletimSerie serie1, BoletimSerie serie2)
        {
            return !(serie1 == serie2);
        }
    }
}