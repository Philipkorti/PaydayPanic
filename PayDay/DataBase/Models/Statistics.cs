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
    public class Statistics
    {

        /// <summary> Gets or sets the number of times the player has played. </summary>
        [BsonElement("GameCount"), BsonRepresentation(BsonType.Int32)]
        public int GameCount { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player wins. </summary>
        [BsonElement("GameMoneyWin"), BsonRepresentation(BsonType.Double)]
        public double GameMoneyWin { get; set; }

        /// <summary> Gets or sets the amount of money the player lose. </summary>
        [BsonElement("GameMoneyLose"), BsonRepresentation(BsonType.Double)]
        public double GameMoneyLose { get; set; }

        [BsonElement("Casino")]
        public List<Casino> Casino { get; set; }

        [BsonElement("Shop")]
        public List<Shop> Shop { get; set; }
    }
}
