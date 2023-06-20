using Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Items
    {
        /// <summary> Gets or sets the itemID. </summary>
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ItemID { get; set; }

        /// <summary> Gets or sets the PictureURL of the product. </summary>
        [StringLength(ConstData.StringLengh), BsonElement("PictureURL"), BsonRepresentation(BsonType.String)] 
        public string PictureURL { get; set; }

        /// <summary> Gets or sets the title of the product. </summary>
        [StringLength(ConstData.StringLengh), BsonElement("Title"), BsonRepresentation(BsonType.String)] 
        public string Title { get; set; }

        /// <summary> Gets or sets the price of the product. </summary>
        [BsonElement("Price"), BsonRepresentation(BsonType.Double)]
        public double Price { get; set; }

        /// <summary> Gets or sets the number of products in stock. </summary>
        [BsonElement("InStock"), BsonRepresentation(BsonType.Int32)]
        public int InStock { get; set; }
    }
}