using Data;
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
    public class User
    {
        /// <summary> Gets or set the id of the player. </summary>
        [BsonId, BsonElement("UserId"), BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary> Gets or sets the username of the player. </summary>
        [StringLength(ConstData.StringLengh), BsonElement("UserName"), BsonRepresentation(BsonType.String)] 
        public string UserName { get; set; }

        /// <summary> Gets or sets the password of the player. </summary>
        [BsonElement("Password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; }

        [BsonElement("Statistics")]
        public List<Statistics> Statistics { get; set; }

        [BsonElement("Highscore")]
        public List<Highscore> Highscore { get; set; }

    }
}
