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
    class Pizza
    {
        public ObjectId Id { get; set; }
        public String name { get; set; }
        public String ingredients { get; set; }
        public PizzaSizes[] sizes { get; set;}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("" +
                "\nID: {0}" +
                "\nname: {1}" +
                "\ningredients: {2}" +
                "\n"
                , Id
                , name
                , ingredients);

            sb.AppendLine("\n" + "Die Variationen:");

            foreach (var pizzasizes in sizes) {
                sb.Append("[");
                sb.AppendLine("\n" + "Name: " + pizzasizes.name.ToString());
                sb.AppendLine("Diameter: " + pizzasizes.diamater.ToString() + "cm");
                sb.AppendLine("KCAL: " + pizzasizes.kcal.ToString() + "kcal");
                sb.AppendLine("Price: " + pizzasizes.price.ToString() + "CHF");
                sb.Append("]");
            }

            sb.AppendLine("");

            return sb.ToString();
        }
    }
}
