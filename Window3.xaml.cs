using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Projekt1._0
{
    
    public partial class Window3 : Window
    {
        List<Kunde> kunden = new List<Kunde>();
        static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = client.GetDatabase("PizzaDB");
        public Window3()
        {
            InitializeComponent();
            var filter = new BsonDocument();
            var collectionKunde = database.GetCollection<BsonDocument>("Kunden");
            var documentsKunde = collectionKunde.Find(filter).ToList();

            kunden = documentsKunde.Select(v => new Kunde(
                 _id: v["_id"].AsObjectId,
                 Nachname: v["Nachname"].AsString,
                 Vorname: v["Vorname"].AsString,
                 Strasse: v["Strasse"].AsString,
                 PLZ: v["PLZ"].AsString,
                 Ort: v["Ort"].AsString,
                 Telefon: v["Telefon"].AsString,
                 EMail: v["EMail"].AsString,
                 Geburtstag: v["Geburtstag"].AsDateTime)
            ).ToList();
            AddRange( listbox1,kunden);
        }

        private void InsertKunde_Click(object sender, RoutedEventArgs e)
        {
            MongoClient client = new MongoClient("mongodb://127.0.0.1:27017/");

            IMongoDatabase db = client.GetDatabase("PizzaDB");
            IMongoCollection<BsonDocument> collection = 
                db.GetCollection<BsonDocument>("Kunden");
            Kunde kunde = new Kunde();
            kunde.Nachname = tbxNachname.Text;
            kunde.Vorname = tbxVorname.Text;
            kunde.Strasse = tbxStrasse.Text;
            kunde.PLZ = tbxPLZ.Text;
            kunde.Ort = tbxOrt.Text;
            kunde.Telefon = tbxTelefon.Text;
            kunde.EMail = tbxEmail.Text;
            kunde.Geburtstag = datePicker.SelectedDate.Value;
            
            BsonDocument doc = kunde.ToBsonDocument();
            collection.InsertOne(doc);
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = listbox1.SelectedIndex;
                if (index > -1)
                {
                    Kunde kunde = kunden[index];
                    tbxNachname.Text = kunde.Nachname;
                    tbxVorname.Text = kunde.Vorname;
                    tbxStrasse.Text = kunde.Strasse;
                    tbxPLZ.Text = kunde.PLZ;
                    tbxOrt.Text = kunde.Ort;
                    tbxTelefon.Text = kunde.Telefon;
                    tbxEmail.Text = kunde.EMail;
                    datePicker.Text = kunde.Geburtstag.Date.ToString(); 
                }
            }
            catch (Exception ex) { }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddRange(ListBox listbox1, List<Kunde> list)
        {
            listbox1.Items.Clear();
            foreach (Kunde item in list)
            {
                listbox1.Items.Add(item);
            }
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show($"Löschung von {kunden[listbox1.SelectedIndex].Vorname} bestätigen", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var collection = database.GetCollection<BsonDocument>("Kunden");
                    var fitler = Builders<BsonDocument>.Filter.Eq("_id", kunden[listbox1.SelectedIndex]._id);

                    collection.DeleteOne(fitler);
                }
            }
        }
    }
}
