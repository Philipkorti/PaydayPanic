using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Statistics
    {
        [Key] public int StatisticID { get; set; }
        public User UserID { get; set; }

        [Required] public int GameCount { get; set; } = 0;
    }
}
