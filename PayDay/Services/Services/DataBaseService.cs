using Data;
using DataBase.Context;
using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DataBaseService
    {
        public static IMongoCollection<User> DBConection()
        {
            var client = new MongoClient(ConstData.DataBaseCon);
            var db = client.GetDatabase("PayDay");
            var productCollection = db.GetCollection<User>("User");
            return productCollection;
        }
        public static void PlusGame(Game game)
        {
            var productCollection = DataBaseService.DBConection();
            var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
            var uodate = Builders<User>.Update.Inc(a => a.Statistics[0].GameCount, 1);
            var user = productCollection.UpdateOne(filter, uodate);
        }
        public static void PlusWinsCasino(Game game, int sevenWin, int heartWins, int paydayWins, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].Casino[0].WinSeven, sevenWin)
                    .Inc(a => a.Statistics[0].Casino[0].WinMoneyBag,paydayWins)
                    .Inc(a => a.Statistics[0].Casino[0].WinHeart, heartWins)
                    .Inc(a => a.Statistics[0].Casino[0].GameCasinoCount,1)
                    .Inc(a => a.Statistics[0].Casino[0].CasinoWinCount,1);
                var user = productCollection.UpdateOne(filter, update);
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].Casino[0].GameCasinoCount, 1);
                var user = productCollection.UpdateOne(filter, update);
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update
                    .Inc(a => a.Statistics[0].Shop[0].BoughtGamesCount, 1)
                    .Inc(a => a.Statistics[0].Shop[0].OutputMoney, money);
                var user = productCollection.UpdateOne(filter, update);
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update
                    .Inc(a => a.Statistics[0].Shop[0].InputMoney, money)
                    .Inc(a => a.Statistics[0].Shop[0].GamesSoldCount,1);
                var user = productCollection.UpdateOne(filter, update);
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Set(a => a.Highscore[0].Rank, rankurl).Set(a => a.Highscore[0].Elo, newElo);
                var user = productCollection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

        public static void LoadStatistic(out StatisticData statisticData, out ErrorCodes errorCodes, Game game)
        {
            statisticData = null;
            try
            {
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var user = productCollection.Find(filter).ToList();
                statisticData = new StatisticData
                {
                    BoughtGamesCount = user[0].Statistics[0].Shop[0].BoughtGamesCount,
                    CasinoCount = user[0].Statistics[0].Casino[0].GameCasinoCount,
                    CasinoWins = user[0].Statistics[0].Casino[0].CasinoWinCount,
                    GameMoneyLoos = user[0].Statistics[0].GameMoneyLose,
                    GameCount = user[0].Statistics[0].GameCount,
                    GameMoneyWin = user[0].Statistics[0].GameMoneyWin,
                    Rank = user[0].Highscore[0].Rank,
                    Elo = user[0].Highscore[0].Elo,
                    SoldGamesCount = user[0].Statistics[0].Shop[0].GamesSoldCount,
                    InPutMoney = user[0].Statistics[0].Shop[0].InputMoney,
                    OutputMoney = user[0].Statistics[0].Shop[0].OutputMoney,
                    WinHeart = user[0].Statistics[0].Casino[0].WinHeart,
                    WinMoneybag = user[0].Statistics[0].Casino[0].WinMoneyBag,
                    WinSeven = user[0].Statistics[0].Casino[0].WinSeven
                };
                errorCodes = ErrorCodes.NoError;
            }
            catch
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
        }
        public static void LoadHighscore(List<HighscoreViewData> highscore, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Empty;
                var user = productCollection.Find(filter).Limit(20).SortByDescending(a => a.Highscore[0].Elo).ToList();
                foreach(var item in user)
                {
                    highscore.Add(new HighscoreViewData { UserName = item.UserName, Elo = item.Highscore[0].Elo, RankURL = item.Highscore[0].Rank });
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].Shop[0].OutputMoney, money);
                var user = productCollection.UpdateOne(filter, update);
                
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
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].Shop[0].InputMoney, money);
                var user = productCollection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
           
        }

        public static void OutPutMoney(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].GameMoneyLose, money);
                var user = productCollection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
        }

        public static void InputPutMoney(Game game, double money, out ErrorCodes errorCodes)
        {
            errorCodes = new ErrorCodes();
            try
            {
                var productCollection = DataBaseService.DBConection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, game.Username);
                var update = Builders<User>.Update.Inc(a => a.Statistics[0].GameMoneyWin, money);
                var user = productCollection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }
        }

    }
}
