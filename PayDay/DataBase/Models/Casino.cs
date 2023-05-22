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
        /// <summary> Gets or sets the id of the player. </summary>
        [Key] public int Id { get; set; }

        /// <summary> Gets or sets the id of the statistics table.  </summary>
        [ForeignKey("Statistics")]
        public int StatisticsID { get; set; }
        /// <summary> Gets or sets the foreignkey of the statistics table. </summary>
        public Statistics Statistics { get; set; }

        /// <summary> Gets or sets the number of times the player has played in the casino. </summary>
        [Required] public int CasinoWinCount { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has played in the casino. </summary>
        [Required] public int GameCasinoCount { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the seven in the casino. </summary>
        [Required] public int WinSeven { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the moneybag in the casino. </summary>
        [Required] public int WinMoneyBag { get; set; } = 0;

        /// <summary> Gets or sets the number of times the player has win with the heart in the  casino. </summary>
        [Required] public int WinHeart { get; set; } = 0;
    }
}
