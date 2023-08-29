using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace PizzashopVerwaltung
{
    class Bestellposition
    {
        public Pizza pizza { get; set; }
        public double Preis { get; set; }
        public int Stückzahl { get; set; }
        public Extrazutat[] extrazutaten { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{{" +
                "Pizza: {0}" +
                "Preis: {1}" +
                "Stückzahl: {2}" +
                "Extrazutaten: {3}"
                , pizza.ToString()
                , Preis
                , Stückzahl
                , extrazutaten
                );

            return sb.ToString();
        }
    }
}
