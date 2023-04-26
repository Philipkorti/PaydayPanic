using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Shop
    {
        [Key] public int Id { get; set; }

        public Statistics StatisticsID { get; set; }

        [Required] public int BoughtGamesCount { get; set; } = 0;

        [Required] public int GamesSoldCount { get; set; } = 0;

        [Required] public double OutputMoney { get; set; } = 0;

        [Required] public double InputMoney { get; set; } = 0;
    }
}
