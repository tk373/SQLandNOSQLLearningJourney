using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace PizzashopVerwaltung
{
    class Kunde
    {
        public ObjectId Id { get; set; }
        public String Vorname { get; set; }
        public String Nachname { get; set; }
        public String StrasseUndNUmmer { get; set; }
        public String PLZ { get; set; }
        public String Ort { get; set; }
        public String Telefonnrm { get; set; }
        public String Email { get; set; }
        public DateTime Birthday { get; set; }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("\n" +
                "\nID: {0}" +
                "\nName: {1}," +
                "\nNachname: {2}," +
                "\nStrasseUndNUmmer: {3}," +
                "\nOrt: {4}," +
                "\nPLZ: {5}," +
                "\nTelefonNmr: {6}," +
                "\nEmail: {7}," +
                "\nGeburtstag: {8}\n",
                Id,
                Vorname,
                Nachname,
                StrasseUndNUmmer,
                Ort,
                PLZ,
                Telefonnrm,
                Email,
                Birthday.ToLocalTime().ToShortDateString());

            return sb.ToString();
        }

    }
}
