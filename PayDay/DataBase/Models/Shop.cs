using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Shop
    {
        /// <summary> Gets or sets the id of the shop. </summary>
        [Key] public int Id { get; set; }

        /// <summary> Gets or sets the statisticsid of the statistics table. </summary>
        [ForeignKey("Statistics")]
        public int StatisticsID { get; set; }

        /// <summary> Gets or sets the foreignKey of the statistics table. </summary>
        public Statistics Statistics { get; set; }

        /// <summary> Gets or sets the count of bought games. </summary>
        [Required] public int BoughtGamesCount { get; set; } = 0;

        /// <summary> Gets or sets the count of sold games. </summary>
        [Required] public int GamesSoldCount { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player have spent. </summary>
        [Required] public double OutputMoney { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player have taken. </summary>
        [Required] public double InputMoney { get; set; } = 0;
    }
}
