using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Gold
    {
        [Key]
        public int id { get; set; }
        public double Price { get; set; }
    }
}
