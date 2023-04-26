using Data;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Items
    {
        [Key] public int ItemID { get; set; }

        [Required] [StringLength(25)] public string PictureURL { get; set; }

        [Required] [StringLength(ConstData.StringLengh)] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public double Price { get; set; }

        [Required] public int InStock { get; set; }
    }
}