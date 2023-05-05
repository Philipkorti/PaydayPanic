using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ItemsGame
    {
        public int ItemID { get; set; }

        public string PictureURL { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price
        {
            get; set;
        }
    }
}
