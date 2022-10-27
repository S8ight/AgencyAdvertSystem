using Application.Common.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class AgencyDbConnection : IAgencyDbConnection
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "AgencyAdvertSystem";
        private const string AdvertCollection = "Advert";
        private const string PhotoListCollection = "PhotoList";
        private const string SaveListCollection = "SaveList";
        private const string UserCollection = "Users";

        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }
    }
}
