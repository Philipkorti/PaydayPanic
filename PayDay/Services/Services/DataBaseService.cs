using Data;
using DataBase.Context;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DataBaseService
    {
        public static void PlusWinsCasino(Game game, int sevenWin, int heartWins, int paydayWins, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var casino = context.Casino.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    casino[0].WinSeven = casino[0].WinSeven + sevenWin;
                    casino[0].WinHeart = casino[0].WinHeart + heartWins;
                    casino[0].WinMoneyBag = casino[0].WinMoneyBag + paydayWins;
                    casino[0].GameCasinoCount++;
                    casino[0].CasinoWinCount++;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }
        public static void LoosPlusCasino(Game game, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var casino = context.Casino.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    casino[0].GameCasinoCount++;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }
        public static void BuyGamePlusDatabase(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var shop = context.Shop.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    shop[0].OutputMoney += money;
                    shop[0].BoughtGamesCount++;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }
        public static void SellGamePlusDatabase(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var shop = context.Shop.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    shop[0].InputMoney += money;
                    shop[0].GamesSoldCount++;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

        /// <summary>
        /// Save player data in the database.
        /// </summary>
        public static void SaveData(int newElo,Game game, out ErrorCodes errorCodes)
        {
            string rankurl = GameEndServices.RankUpgrade(newElo);
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var items = context.Highscore.Where(f => f.User.UserName == game.Username).ToList();
                    var rank = context.Ranks.Where(f => f.RankURL == rankurl).ToList();
                    items[0].Elo = newElo;
                    items[0].RankID = rank[0].Id;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

        public static void LoadHighscore(List<HighscoreViewData> highscore, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {

                    var item = (from high in context.Highscore
                                join u in context.User on high.UserID equals u.UserId
                                join r in context.Ranks on high.RankID equals r.Id
                                where u.UserId == high.UserID
                                select new
                                {
                                    u.UserId,
                                    u.UserName,
                                    r.Rank,
                                    r.RankURL,
                                    high.Elo
                                }).OrderByDescending(x => x.Elo).Take(20).ToList();

                    foreach (var high in item)
                    {
                        highscore.Add(new HighscoreViewData() { UserID = high.UserId, UserName = high.UserName, Rank = high.Rank, Elo = high.Elo, RankURL = high.RankURL });
                    }

                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }
        public static void BuyGoldPlusDatabase(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var shop = context.Shop.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    shop[0].OutputMoney += money;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

        public static void SellGoldPlusDatabase(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                using (var context = new PayDayContext())
                {
                    var shop = context.Shop.Where(f => f.Statistics.User.UserName == game.Username).ToList();
                    shop[0].InputMoney += money;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

    }
}
