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
    class Extrazutat
    {
        public String name { get; set; }
        public Double preis { get; set; }
    }
}
