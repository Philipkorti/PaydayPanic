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
        /// <summary> Gets or sets the id of the highcore table. </summary>
        [Key] public int Id { get; set; }

        /// <summary> Gets or sets the elo of the player. </summary>
        [Required] public int Elo { get; set; } 

        /// <summary> Gets or sets the UserID of the player. </summary>
        [ForeignKey("User")]
        public int UserID { get; set; }

        /// <summary> Gets or sets the foreignKey of the User table. </summary>
        public User User { get; set; }

        /// <summary> Gets or sets the rankID of the rank table. </summary>
        [ForeignKey("Ranks")]
        public int RankID { get; set; }

        /// <summary>
        /// Gets or sets the foreignKey of the Rank table.
        /// </summary>
        public Ranks Ranks { get; set; }

    }
}
