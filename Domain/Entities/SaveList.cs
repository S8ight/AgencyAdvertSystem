using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class SaveList
    {
        [BsonId]
        public string ListID { get; set; }

        public string AdvertID { get; set; }

        public string UserID { get; set; }
    }
}
