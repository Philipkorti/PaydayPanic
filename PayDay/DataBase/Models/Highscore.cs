using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Highscore
    {
        [Key] public int Id { get; set; }
        public User UserID { get; set; }
        [Required] public double HighestScore { get; set; } = 0;
    }
}
