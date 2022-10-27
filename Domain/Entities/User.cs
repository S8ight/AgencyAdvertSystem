using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        
        public byte[] Photo { get; set; }
    }
}
