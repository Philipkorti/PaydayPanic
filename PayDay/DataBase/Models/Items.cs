using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Items
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int InStock { get; set; }
    }
}