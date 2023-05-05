using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            }
            else
            {
                goldprice = game.GoldPrice * goldprice;
                game.GoldPrice = Math.Round(game.GoldPrice - goldprice,2);
            }
            return game;
        }
    }
}
