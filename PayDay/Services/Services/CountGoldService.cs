using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CountGoldService
    {

        public static Game GoldpriceCount(Game game)
        {
            Random random = new Random();
            double goldprice = random.NextDouble();
            if(random.Next(0,2) == 1)
            {
                goldprice = game.GoldPrice * goldprice;
                game.GoldPrice = Math.Round(game.GoldPrice + goldprice,2);
                game.GoldPrice = game.GoldPrice > 100 ? 100 : game.GoldPrice;
            }
            else
            {
                goldprice = game.GoldPrice * goldprice;
                game.GoldPrice = Math.Round(game.GoldPrice - goldprice,2);
                game.GoldPrice = game.GoldPrice < 10 ? 10 : game.GoldPrice; 
            }
            return game;
        }
        public static void GamePriceCalculate(ObservableCollection<ShopItems> shopItems)
        {
            Random random = new Random();
            double gameprice = random.NextDouble();
            foreach (var item in shopItems)
            {
                gameprice = random.NextDouble();
                if (random.Next(0,2)==1)
                {
                    item.Price += item.Price * gameprice;
                }
                else
                {
                    item.Price -= item.Price * gameprice;
                    item.Price = item.Price <=10 ? 10 : item.Price;
                }
            }
        }
    }
}
