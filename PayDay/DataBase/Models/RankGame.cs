using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class RankGame
    {
        [BsonId,BsonElement("UserId"), BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("PlayerOneId"), BsonRepresentation(BsonType.ObjectId)]
        public string PlayerOneId { get; set; }

        [BsonElement("PlayerTwoId"),BsonRepresentation(BsonType.ObjectId)]
        public string PlayerTwoId { get; set;}

        [BsonElement("PlayerOneReady"), BsonRepresentation(BsonType.Boolean)]
        public bool PlayerOneRady { get; set; }

        [BsonElement("PlayerTwoReady"), BsonRepresentation(BsonType.Boolean)]
        public bool PlayerTwoRady { get; set;}

        [BsonElement("PlayerOneMoney"), BsonRepresentation(BsonType.Int64)]
        public int PlayerOneMoney { get; set; }

        [BsonElement("PlayerTwoMoney"), BsonRepresentation(BsonType.Int64)]
        public int PlayerTwoMoney { get; set; }
    }
}
