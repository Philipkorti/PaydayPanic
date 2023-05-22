using Data;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Items
    {
        /// <summary> Gets or sets the itemID. </summary>
        [Key] public int ItemID { get; set; }

        /// <summary> Gets or sets the PictureURL of the product. </summary>
        [Required] [StringLength(ConstData.StringLengh)] public string PictureURL { get; set; }

        /// <summary> Gets or sets the title of the product. </summary>
        [Required] [StringLength(ConstData.StringLengh)] public string Title { get; set; }

        /// <summary> Gets or sets the price of the product. </summary>
        [Required] public double Price { get; set; }

        /// <summary> Gets or sets the number of products in stock. </summary>
        [Required] public int InStock { get; set; }
    }
}