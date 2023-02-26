using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

namespace Projekt1._0
{
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        List<Bestellung> bestellungen = new List<Bestellung>();
        static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = client.GetDatabase("PizzaDB");
        public Window2()
        {
            InitializeComponent();
            var filter = new BsonDocument();
            var collectionBestellung = database.GetCollection<BsonDocument>("Bestellung");
            var documentsBestellung = collectionBestellung.Find(filter).ToList();
            
            bestellungen = documentsBestellung.Select(v => new Bestellung(
               _id: v["_id"].AsObjectId,
               Bestellnummer: v["Bestellnummer"].AsString,
               Bestelldatum: v["Bestelldatum"].AsDateTime,
               Kunde: v["Kunde"].AsString,
               Pizza: v.Contains("Pizza") ? v["Pizza"].ToString() : "",
               Extrazutaten: v.Contains("Extrazutaten") ? v["Extrazutaten"].ToString() : "",
               Preis: v.Contains("Preis") ? v["Preis"].ToString() : "",
               Stückzahl: v.Contains("Stückzahl") ? v["Stückzahl"].ToString() : "",
               Total: v["Total"].AsInt32)
           ).ToList();
            AddRange(listboxBestellung, bestellungen);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listboxBestellung.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show($"Löschung von {bestellungen[listboxBestellung.SelectedIndex].Bestellnummer} bestätigen", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var collection = database.GetCollection<BsonDocument>("Bestellung");
                    var fitler = Builders<BsonDocument>.Filter.Eq("_id", bestellungen[listboxBestellung.SelectedIndex]._id);

                    collection.DeleteOne(fitler);
                }
            }
        }
        private void AddRange(ListBox listboxBestellung, List<Bestellung> list)
        {
            listboxBestellung.Items.Clear();
            foreach (Bestellung item in list)
            {
                listboxBestellung.Items.Add(item);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MongoClient client = new MongoClient("mongodb://127.0.0.1:27017/");

            IMongoDatabase db = client.GetDatabase("PizzaDB");
            IMongoCollection<BsonDocument> collection =
                db.GetCollection<BsonDocument>("Bestellung");
            Bestellung bestellung = new Bestellung();
            bestellung.Bestellnummer = tbxBestellnummer.Text;
            bestellung.Bestelldatum = datePicker.SelectedDate.Value;
            bestellung.Kunde = tbxKunde.Text;
            bestellung.Pizza = tbxPizza.Text;
            bestellung.Extrazutaten = tbxExtra.Text;
            bestellung.Preis = tbxPreis.Text;
            bestellung.Stückzahl = tbxStueckzahl.Text;
            

            BsonDocument doc = bestellung.ToBsonDocument();
            collection.InsertOne(doc);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void listboxBestellung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                int index = listboxBestellung.SelectedIndex;
                if (index > -1)
                {
                    Bestellung bestellung = bestellungen[index];
                    tbxBestellnummer.Text = bestellung.Bestellnummer;
                    datePicker.Text = bestellung.Bestelldatum.Date.ToString();
                    tbxKunde.Text = bestellung.Kunde;
                    tbxPizza.Text = bestellung.Pizza;
                    tbxExtra.Text = bestellung.Extrazutaten;
                    tbxPreis.Text = bestellung.Preis;
                    tbxStueckzahl.Text = bestellung.Stückzahl;
                    tbxTotal.Text = bestellung.Total.ToString();
                    
                }
            }
            catch (Exception ex) { }
        }
    }
}
