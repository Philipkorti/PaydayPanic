using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Shop
    {

        /// <summary> Gets or sets the count of bought games. </summary>
        [BsonElement("BoughtGamesCount"), BsonRepresentation(BsonType.Int32)]
        public int BoughtGamesCount { get; set; } = 0;

        /// <summary> Gets or sets the count of sold games. </summary>
        [BsonElement("GamesSoldCount"), BsonRepresentation(BsonType.Int32)]
        public int GamesSoldCount { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player have spent. </summary>
        [BsonElement("OutputMoney"), BsonRepresentation(BsonType.Double)]
        public double OutputMoney { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player have taken. </summary>
        [BsonElement("InputMoney"), BsonRepresentation(BsonType.Double)]
        public double InputMoney { get; set; } = 0;
    }
}
