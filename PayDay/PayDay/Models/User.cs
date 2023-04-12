using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(ConstData.StringLengh)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Elo { get; set; } = null;

        public int highscore { get; set; } = 0;

        public int Goldscore { get; set; } = 0;

        public int GameCount { get; set; } = 0;

        public DateTime GameTime { get; set; }

        public ICollection<Items> Items { get; set; } = null;

    }
}
