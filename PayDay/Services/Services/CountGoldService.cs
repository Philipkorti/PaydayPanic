using Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int money = random.Next(0, 220);
            double goldprice = Convert.ToDouble(money) + random.NextDouble();
            game.GoldPrice = Math.Round(goldprice, 2);
            return game;
        }
    }
}
