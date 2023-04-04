using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int Elo { get; set; } = 0;

        public int highscore { get; set; } = 0;

        public int Goldscore { get; set; } = 0;

        public ICollection<Items> Items { get; set; } = null;
    }
}
