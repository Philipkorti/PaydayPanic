using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Statistics
    {
        [Key] public int StatisticID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        [Required] public int GameCount { get; set; } = 0;
        [Required] public double GameMoneyWin { get; set; }
        [Required] public double GameMoneyLose { get; set; }
    }
}
