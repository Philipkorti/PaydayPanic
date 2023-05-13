using Common.Command.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   
    public class ShopItems : NotifyPropertyChanged
    {
        private int inStock;
        private double price;
        public int ItemID { get; set; }

        public string PictureURL { get; set; }

        public string Title { get; set; }

        public double Price 
        { 
            get 
            { 
                return Math.Round(this.price); 
            } 
            set 
            { 
                this.price = value;
                this.OnPropertyChanged(nameof(this.Price));
            } 
        }

        public int InStock 
        {
            get { return inStock; }
            set 
            { 
                inStock = value;
                this.OnPropertyChanged(nameof(InStock));
            }
        }
    }
}
