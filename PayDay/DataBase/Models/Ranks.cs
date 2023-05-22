using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Ranks
    {
        /// <summary> Gets or sets the id of the rank. </summary>
        [Key] public int Id { get; set; }

        /// <summary> Gets or sets the rank of the player. </summary>
        public string Rank { get; set; }

        /// <summary> Gets or set the picture url of the rank. </summary>
        public string RankURL { get; set; }
    }
}
