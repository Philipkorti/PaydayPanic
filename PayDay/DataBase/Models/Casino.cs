using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Casino
    {
        [Key] public int Id { get; set; }

        public Statistics StatisticsID { get; set; }

        [Required] public int WinCount { get; set; } = 0;

        [Required] public int GameCount { get; set; } = 0;

        [Required] public int WinSeven { get; set; } = 0;

        [Required] public int WinMoneyBag { get; set; } = 0;

        [Required] public int WinHeart { get; set; } = 0;
    }
}
