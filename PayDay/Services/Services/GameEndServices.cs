using Data;
using DataBase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GameEndServices
    {
       public static int CountElo(Game game, out int elo)
        {
            double averageMonyWin = 0;
            double averageMonyLose = 0;
            int newelo = 0;
            elo = 0;
            using(var context = new PayDayContext()) 
            {
                var item = (from high in context.Highscore
                        join u in context.User on high.UserID equals u.UserId
                        join s in context.Statistics on u.UserId equals s.UserID
                        where u.UserName == game.Username
                        select new
                        {
                            high.Elo,
                            s.GameMoneyWin,
                            s.GameMoneyLose,
                            s.GameCount
                        }).ToList();
                elo = item[0].Elo;
                if (item[0].GameCount !=0)
                {
                    averageMonyWin = item[0].GameMoneyWin / item[0].GameCount;
                    averageMonyLose = item[0].GameMoneyLose / item[0].GameCount;
                }
            }
            if (game.MoneyWin > averageMonyWin)
            {
                newelo = Convert.ToInt32(Math.Round((game.MoneyWin - averageMonyWin)/4, 0));
            }
            return newelo;
        }

        public static void SaveDateaStatic(Game game)
        {
            using(var context = new PayDayContext())
            {
                var item = context.Statistics.Where(s => s.User.UserName == game.Username).ToList();

                item[0].GameMoneyLose += game.Moneylose;
                item[0].GameMoneyWin += game.MoneyWin;
                context.SaveChanges();
            }
        }
        public static string RankUpgrade(int elo)
        {
            string rank = @"\Pictures\";
            if (500 > elo)
            {
                rank += "bronze.png";
            }
            else if(elo >= 500 && elo < 1000)
            {
                rank += "silver.png";
            }else if(elo >= 1000 && elo < 1500)
            {
                rank += "gold.png";
            }else if(elo >= 1500 && elo < 2000)
            {
                rank += "platin.png";
            }else if(elo >= 2000 && elo < 2500)
            {
                rank += "dimond.png";
            }else if(elo >= 2500 && elo < 3000)
            {
                rank += "master.png";
            }else
            {
                rank += "grandmaster.png";
            }
            return rank;
        }
    }

}
