using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

namespace Projekt1._0
{

    public partial class Window1 : Window
    {

        List<Pizza> pizzas = new List<Pizza>();
        static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = client.GetDatabase("PizzaDB");
        public Window1()
        {
            InitializeComponent();
            string[] groessen = { "Small", "Medium", "Large" };
            cbxOk.ItemsSource = groessen;
            string[] extrazutaten = { "Pommes + 1.00 FR", "Tomaten + 3.00 FR", "Fleisch + 1.00 FR", "Shrimps + 3.00 FR"
            ,"Bacon + 2.00 FR", "Jalapenos + 3.00 FR","Spinach + 2.00 FR"};
            cbx2.ItemsSource = extrazutaten;
            var filter = new BsonDocument();
            var collectionPizza = database.GetCollection<BsonDocument>("Pizza");
            var documentsPizza = collectionPizza.Find(filter).ToList();
            pizzas = documentsPizza.Select(v => new Pizza(
                _id: v["_id"].AsObjectId,
                Name: v["Name"].AsString,
                Zutaten: v["Zutaten"].AsString,
                KCAL: v["KCAL"].AsString,
                Groesse: v["Groesse"].AsString,
                Durchmesser: v["Durchmesser"].AsString,
                Extrazutaten: v["Extrazutaten"].AsString,
                Einzelpreis: v["Einzelpreis"].AsString)
            ).ToList();

            AddRange(lblX, pizzas);
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InsertPizza_Click(object sender, RoutedEventArgs e)
        {
            MongoClient client = new MongoClient("mongodb://127.0.0.1:27017/");

            IMongoDatabase db = client.GetDatabase("PizzaDB");
            IMongoCollection<BsonDocument> collection =
                db.GetCollection<BsonDocument>("Pizza");
            Pizza pizza = new Pizza();
            pizza.Name = tbxName.Text;
            pizza.Zutaten = tbxZutaten.Text;
            pizza.KCAL = tbxKCAL.Text;
            pizza.Groesse = cbxOk.Text;
            pizza.Durchmesser = tbxDurchmesser.Text;
            pizza.Extrazutaten = cbx2.Text;
            pizza.Einzelpreis = tbxEinzelPreis.Text;


            BsonDocument bam = pizza.ToBsonDocument();
            collection.InsertOne(bam);



            pizzas.Add(new Pizza(tbxName.Text, tbxZutaten.Text, tbxZutaten.Text, cbxOk.Text, tbxDurchmesser.Text, cbx2.Text, tbxEinzelPreis.Text));

            AddRange(lblX, pizzas);
        }

        private void AddRange(ListBox listBox, List<Pizza> list)
        {
            listBox.Items.Clear();
            foreach (Pizza item in list)
            {
                listBox.Items.Add(item);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxOk.SelectedItem.ToString() == "Small")
            {
                tbxDurchmesser.Text = "24cm";
            }
            if (cbxOk.SelectedItem.ToString() == "Medium")
            {
                tbxDurchmesser.Text = "30cm";
            }
            if (cbxOk.SelectedItem.ToString() == "Large")
            {
                tbxDurchmesser.Text = "40cm";
            }
            if (cbxOk.SelectedItem.ToString() == "Small")
            {
                tbxEinzelPreis.Text = "15.00 Fr.";
            }
            if (cbxOk.SelectedItem.ToString() == "Medium")
            {
                tbxEinzelPreis.Text = "20.00 Fr.";
            }
            if (cbxOk.SelectedItem.ToString() == "Large")
            {
                tbxEinzelPreis.Text = "30.00 Fr.";
            }
            
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            double result = 15.00;
            double result1 = 20.00;
            double result2 = 30.00;
            

            if (cbx2.SelectedItem.ToString() == "Pommes + 1.00 FR")
            {
                result += 1.00;
            }
            if (cbx2.SelectedItem.ToString() == "Tomaten + 3.00 FR")
            {
                result += 3.00;
            }
            if (cbx2.SelectedItem.ToString() == "Fleisch + 1.00 FR")
            {
                result += 1.00;
            }
            if (cbx2.SelectedItem.ToString() == "Shrimps + 3.00 FR")
            {
                result += 3.00;
            }
            if (cbx2.SelectedItem.ToString() == "Bacon + 2.00 FR")
            {
                result += 2.00;
            }
            if (cbx2.SelectedItem.ToString() == "Jalapenos + 3.00 FR")
            {
                result += 3.00;
            }
            if (cbx2.SelectedItem.ToString() == "Spinach + 2.00 FR")
            {
                result += 2.00;
            }
            tbxEinzelPreis.Text = result.ToString();
        }

        private void tbxEinzelPreis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lblX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = lblX.SelectedIndex;
                if (index > -1)
                {
                    Pizza pizza = pizzas[index];
                    tbxName.Text = pizza.Name;
                    tbxKCAL.Text = pizza.KCAL;
                    tbxDurchmesser.Text = pizza.Durchmesser;
                    tbxEinzelPreis.Text = pizza.Einzelpreis;
                    tbxZutaten.Text = pizza.Zutaten;
                    cbx2.Text = pizza.Zutaten;
                    cbxOk.Text = pizza.Extrazutaten;

                }
            }
            catch (Exception ex) { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lblX.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show($"Löschung von {pizzas[lblX.SelectedIndex].Name} bestätigen", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var collection = database.GetCollection<BsonDocument>("Pizza");
                    var fitler = Builders<BsonDocument>.Filter.Eq("_id", pizzas[lblX.SelectedIndex]._id);

                    collection.DeleteOne(fitler);
                }
            }
        }
    }
}