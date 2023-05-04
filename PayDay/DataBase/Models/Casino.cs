using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Casino
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Statistics")]
        public int StatisticsID { get; set; }
        public Statistics Statistics { get; set; }

        [Required] public int CasinoWinCount { get; set; } = 0;

        [Required] public int GameCasinoCount { get; set; } = 0;

        [Required] public int WinSeven { get; set; } = 0;

        [Required] public int WinMoneyBag { get; set; } = 0;

        [Required] public int WinHeart { get; set; } = 0;
    }
}
