using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Casino
    {

        /// <summary> Gets or sets the number of times the player has played in the casino. </summary>
        [BsonElement("CasinoWinCount"), BsonRepresentation(BsonType.Int32)]
        public int CasinoWinCount { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has played in the casino. </summary>
        [BsonElement("GameCasinoCount"), BsonRepresentation(BsonType.Int32)]
        public int GameCasinoCount { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the seven in the casino. </summary>
        [BsonElement("WinSeven"), BsonRepresentation(BsonType.Int32)]
        public int WinSeven { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the moneybag in the casino. </summary>
        [BsonElement("WinMoneyBag"), BsonRepresentation(BsonType.Int32)]
        public int WinMoneyBag { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the heart in the  casino. </summary>
        [BsonElement("WinHeart"), BsonRepresentation(BsonType.Int32)] 
        public int WinHeart { get; set; } = 0;
    }
}
