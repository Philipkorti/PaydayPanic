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
        [Key] public int Id { get; set; }
        
        public string Rank { get; set; }
        public string RankURL { get; set; }
    }
}
