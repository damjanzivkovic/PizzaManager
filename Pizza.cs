using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1._0
{
    public class Pizza
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Zutaten { get; set; }
        public string KCAL { get; set; }
        public string Groesse { get; set; }
        public string Durchmesser { get; set; }
        public string Extrazutaten { get; set; }
        public string Einzelpreis { get; set; }
        public string DisplayString { get; set; }

        public Pizza(ObjectId _id, string Name, string Zutaten, string KCAL, string Groesse, string Durchmesser, string Extrazutaten, string Einzelpreis)
        {
            this._id = _id;
            this.Name = Name;
            this.Zutaten = Zutaten;
            this.KCAL = KCAL;
            this.Groesse = Groesse;
            this.Durchmesser = Durchmesser;
            this.Extrazutaten = Extrazutaten;
            this.Einzelpreis = Einzelpreis;

            string zutatenString = string.Join(", ", Zutaten);
            this.DisplayString = $"{Name}: {zutatenString}: {Groesse}:  {Einzelpreis}";
        }

        public Pizza(string Name, string Zutaten, string KCAL, string Groesse, string Durchmesser, string Extrazutaten, string Einzelpreis)
        {
            this.Name = Name;
            this.Zutaten = Zutaten;
            this.KCAL = KCAL;
            this.Groesse = Groesse;
            this.Durchmesser = Durchmesser;
            this.Extrazutaten = Extrazutaten;
            this.Einzelpreis = Einzelpreis;

            string zutatenString = string.Join(", ", Zutaten);
            this.DisplayString = $"{Name}: {zutatenString} {Groesse}    {Einzelpreis}";
        }
        public Pizza()
        {
            string zutatenString = string.Join(", ", Zutaten);
            this.DisplayString = $"{Name}: {zutatenString} {Groesse}    {Einzelpreis}";
        }
    }

    
}
