using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt1._0
{
    public class Kunde
    {
        public ObjectId _id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Strasse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }
        public string EMail { get; set; }
        public DateTime Geburtstag { get; set; }
        public string DisplayString { get; set; }
         
        public Kunde() 
        { }
        public Kunde(ObjectId _id, string Nachname, string Vorname, string Strasse, string PLZ, string Ort,string Telefon, string EMail, DateTime Geburtstag)
        {
            this._id = _id;
            this.Nachname = Nachname;
            this.Vorname = Vorname;
            this.Strasse = Strasse;
            this.PLZ = PLZ;
            this.Ort = Ort; 
            this.Telefon = Telefon;
            this.EMail = EMail;
            this.Geburtstag = Geburtstag;

            string zutatenString = string.Join(", ", Vorname);
            this.DisplayString = $"{Nachname}: {zutatenString}: {Strasse}: {PLZ}:{Ort}: {Telefon}: {EMail}: {Geburtstag}";
        }
        public Kunde(string Nachname, string Vorname, string Strasse, string PLZ, string Ort, string Telefon, string EMail, DateTime Ge
            )
        {
            this.Nachname = Nachname;
            this.Vorname = Vorname;
            this.Strasse = Strasse;
            this.PLZ = PLZ; 
            this.Ort = Ort; 
            this.Telefon = Telefon;
            this.EMail = EMail;
            this.Geburtstag = Geburtstag;
        }
        public Kunde(string text, string text1)
        {
            string zutatenString = string.Join(", ", Vorname);
            this.DisplayString = $"{Nachname}: {zutatenString}: {Strasse}: :{PLZ}:{Ort}: {Telefon}: {EMail}: {Geburtstag}";
        }

    }   
}
