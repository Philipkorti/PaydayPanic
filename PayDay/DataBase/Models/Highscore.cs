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
    public class Highscore
    {

        /// <summary> Gets or sets the elo of the player. </summary>
        [BsonElement("Elo"), BsonRepresentation(BsonType.Int32)]
        public int Elo { get; set; }

        [BsonElement("Rank"), BsonRepresentation(BsonType.String)]
       public string Rank { get; set; }

    }
}
