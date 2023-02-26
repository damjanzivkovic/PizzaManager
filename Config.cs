using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1._0
{
    internal class Config
    {
        public IMongoDatabase getConnection()
        {

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("PizzaDB");

            return database;
        }
    }
}
