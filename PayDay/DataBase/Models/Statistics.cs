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
        /// <summary> Gets or sets the statisticíd. </summary>
        [Key] public int StatisticID { get; set; }

        /// <summary> Gets or sets the userid of the user table. </summary>
        [ForeignKey("User")]
        public int UserID { get; set; }

        /// <summary> Gets or sets the foreignKey of the User table. </summary>
        public User User { get; set; }

        /// <summary> Gets or sets the number of times the player has played. </summary>
        [Required] public int GameCount { get; set; } = 0;

        /// <summary> Gets or sets the amount of money the player wins. </summary>
        [Required] public double GameMoneyWin { get; set; }

        /// <summary> Gets or sets the amount of money the player lose. </summary>
        [Required] public double GameMoneyLose { get; set; }
    }
}
