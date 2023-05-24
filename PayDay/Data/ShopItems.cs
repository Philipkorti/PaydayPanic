using Common.Command.NotifyPropertyChanged;
using System;

namespace Data
{

    public class ShopItems : NotifyPropertyChanged
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary> The number of products in stock. </summary>
        private int inStock;
        /// <summary> The price of the products. </summary>
        private double price;
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary> Gets or sets the itemID. </summary>
        public int ItemID { get; set; }

        /// <summary> Gets or sets the PictureURL of the product. </summary>
        public string PictureURL { get; set; }

        /// <summary> Gets or sets the title of the product. </summary>
        public string Title { get; set; }

        /// <summary> Gets or sets the price of the product. </summary>
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

        /// <summary> Gets or sets the number of products in stock. </summary>
        public int InStock 
        {
            get { return inStock; }
            set 
            { 
                inStock = value;
                this.OnPropertyChanged(nameof(InStock));
            }
        }
        #endregion
    }
}
