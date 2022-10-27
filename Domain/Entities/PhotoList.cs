using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class PhotoList
    {
        [BsonId]
        public string PhotoID { get; set; }

        public string AdvertID { get; set;}

        public string PhotoLink { get; set; }
    }
}
