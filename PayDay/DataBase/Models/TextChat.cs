using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class TextChat
    {
        [BsonId, BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PlayerID { get; set; }
        public string Text { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string GameId { get; set; }
    }
}
