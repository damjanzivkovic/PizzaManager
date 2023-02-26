using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt1._0
{
    public class Bestellung
    {
        public ObjectId _id { get; set; }
        public string Bestellnummer { get; set; }
        public DateTime Bestelldatum { get; set; }
        public string Kunde { get; set; }
        public string Pizza { get; set; }
        public string Extrazutaten { get; set; }
        public string Preis { get; set; }
        public string Stückzahl { get; set; }
        public int Total { get; set; }
        public string DisplayString { get; set; }
        public Bestellung() 
        { 

        }
        public Bestellung(ObjectId _id,string Bestellnummer, DateTime Bestelldatum, string Kunde, string Pizza, string Extrazutaten, string Preis, string Stückzahl, int Total) 
        {
            this._id = _id;
            this.Bestellnummer= Bestellnummer;
            this.Bestelldatum= Bestelldatum;
            this.Kunde= Kunde;
            this.Pizza= Pizza;
            this.Extrazutaten= Extrazutaten;
            this.Preis= Preis;
            this.Stückzahl = Stückzahl;
            this.Total= Total;

            string bestellung=string.Join(", ", Bestellnummer);
            this.DisplayString = $"{Bestelldatum}: {bestellung}: {Kunde}: {Pizza}:{Extrazutaten}: {Preis}: {Stückzahl}: {Total}";
        }
        public Bestellung(string Bestellnummer, DateTime Bestelldatum, string Kunde, string Pizza, string Extrazutaten, string Preis, string Stückzahl, int Total) 
        {
            this.Bestellnummer = Bestellnummer;
            this.Bestelldatum = Bestelldatum;
            this.Kunde = Kunde;
            this.Pizza = Pizza;
            this.Extrazutaten = Extrazutaten;
            this.Preis = Preis;
            this.Stückzahl = Stückzahl;
            this.Total= Total;
        }
        public Bestellung(string text, string text1) 
        {

            string bestellung = string.Join(", ", Bestellnummer);
            this.DisplayString = $"{Bestelldatum}: {bestellung}: {Kunde}: {Pizza}:{Extrazutaten}: {Preis}: {Stückzahl}:{Total}";
        }
    }
}
