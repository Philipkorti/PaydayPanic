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
        [Key] public int Id { get; set; }
        [Required] public double HighestScore { get; set; } = 0;
        [Required] public int Elo { get; set; } 
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Ranks")]
        public int RankID { get; set; }
        public Ranks Ranks { get; set; }

    }
}
