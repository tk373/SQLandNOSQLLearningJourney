using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace PizzashopVerwaltung
{
    public class PizzaSizes
    {
        public string name { get; set; }
        public double diamater { get; set; }
        public double kcal { get; set; }
        public double price { get; set; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(
                "\nname: {0}" +
                "\ndiamater: {1}" +
                "\nkcal: {2}" +
                "\nprice: {3}"
                , name
                , diamater
                , kcal
                , price);

            return sb.ToString(); 
        }
    }
}
