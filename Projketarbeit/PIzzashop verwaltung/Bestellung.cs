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
    class Bestellung
    {
        public ObjectId Id { get; set; }
        public int BestellNummer { get; set; }
        public DateTime Bestelldatum { get; set; }
        public Kunde kunde { get; set; }
        public double Total { get; set; }
        public Bestellposition[] bestellpositionen { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{{\n" +
                "Bestellnummer: {0}\n" +
                "Bestelldatum: {1}\n" +
                "Kunde: {2}\n" +
                "Total: {3}\n" +
                "}}"
                , BestellNummer
                , Bestelldatum.ToLocalTime().ToShortDateString()
                , kunde.ToString()
                , Total
                );

            
            foreach (var bestellpositon in bestellpositionen)
            {
                sb.AppendLine("Pizza: " + bestellpositon.pizza.ToString());
                sb.AppendLine("Gesamtpreis der oben gennanten Position: " + bestellpositon.Preis);
                sb.AppendLine("Stückzahl: " + bestellpositon.Stückzahl);
                sb.AppendLine("Extrazutaten: " + bestellpositon.extrazutaten);
            }
            

            return sb.ToString();
        }
    }
}
