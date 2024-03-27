using Data;
using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Services.Services
{
    public class DataBaseRankeGame
    {
        public static string SetRankGame(string playerOneId, string playerTwoId)
        {
            var rankedCollection = DataBaseService.GetRankedGameCollection();
            if(playerOneId != playerTwoId)
            {
                RankGame rankGame = new RankGame
                {
                    PlayerOneId = playerOneId,
                    PlayerTwoId = playerTwoId,
                    PlayerOneRady = false,
                    PlayerTwoRady = false
                };

                rankedCollection.InsertOne(rankGame);
            }
            return rankedCollection.Find(a=>a.PlayerOneId == playerOneId || a.PlayerTwoId == playerOneId).First().GameId;
        }

        public static bool ReadRankGameSecondPlayer(string userId)
        {
            bool check = true;
            try
            {
                var rankeedCollection = DataBaseService.GetRankedGameCollection();
                var filter = Builders<RankGame>.Filter.Eq(a => a.PlayerTwoId, userId);
                string id = rankeedCollection.Find(filter).First().PlayerTwoId;
            }catch(Exception ex) 
            {
                check = false;
            }
           return check;
        }

        public static string ReadRankGameSecondPlayerByGameId(string gameId, string userid)
        {
            var rankedCollection = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a=>a.GameId, gameId);
            string playertwo = rankedCollection.Find(filter).First().PlayerTwoId;
            if(playertwo == userid)
            {
                playertwo = rankedCollection.Find(filter).First().PlayerOneId;
            }
            return playertwo;
        }
        public static string ReadRankSecondName(string userId)
        {
            var userCollection = DataBaseService.GetUserCollection();
            var filter = Builders<User>.Filter.Eq(a=>a.UserId, userId);
            return userCollection.Find(filter).First().UserName;
        }

        public static void SetRadyRankedGame(string gameId,string userId, bool rady)
        {
            var rankedCollection = DataBaseService.GetRankedGameCollection();
            string playerId = rankedCollection.Find(a=> a.GameId == gameId).First().PlayerOneId;
            var filter = Builders<RankGame>.Filter.Eq(a => a.GameId,gameId);
            
            if (playerId == userId)
            {
                var update = Builders<RankGame>.Update.Set(a => a.PlayerOneRady,rady);
                rankedCollection.UpdateOne(filter,update);
            }
            else
            {
                var update = Builders<RankGame>.Update.Set(a => a.PlayerTwoRady, rady);
                rankedCollection.UpdateOne(filter, update);
            }
            
        }

        public static bool IsRady(string gameId, string userId)
        {
            bool check;
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a=> a.GameId,gameId);
            var item = colRankedGame.Find(filter).First();
            if (item.PlayerOneId == userId)
            {
                check = item.PlayerTwoRady;
            }
            else
            {
                check = item.PlayerOneRady;
            }
            return check;
        }

        public static void DeletRankGame(string rankGameID)
        {
            TextChatService.DeleteMessages(rankGameID);
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a => a.GameId, rankGameID);
            colRankedGame.DeleteOne(filter);
        }

        public static void SetFinishTrue(string rankGameID, string userID)
        {
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a=>a.GameId, rankGameID);
            string PlayerID = colRankedGame.Find(filter).First().PlayerOneId;
            

            if(PlayerID == userID)
            {
                var update = Builders<RankGame>.Update.Set(a => a.PlayerOneFinish, true);
                colRankedGame.UpdateOne(filter, update);
            }
            else
            {
                var update = Builders<RankGame>.Update.Set(a=>a.PlayerTwoFinish, true);
                colRankedGame.UpdateOne(filter, update);
            }
        }

        public static void SetPlayerMoney(string gameId, string playerId, double money)
        {
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a=> a.GameId,gameId);
            string userId = colRankedGame.Find(filter).First().PlayerOneId;
            if (userId == playerId)
            {
                var update = Builders<RankGame>.Update.Set(a => a.PlayerOneMoney,money);
                colRankedGame.UpdateOne(filter,update);
            }
            else
            {
                var update = Builders<RankGame>.Update.Set(a=>a.PlayerTwoMoney,money);
                colRankedGame.UpdateOne(filter, update);
            }
        }

        public static bool ReadSecondPlayerFinish(string gameId, string playeroneId)
        {
            bool check;
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a => a.GameId, gameId);
            var item = colRankedGame.Find(filter).First();
            if (item.PlayerOneId != playeroneId)
            {
                check = item.PlayerOneFinish;
            }
            else
            {
                check = item.PlayerTwoFinish;
            }
            return check;
        }

        public static double ReadSecondPlayerMoney(string gameId, string userid)
        {
            double money;
            var colRankedGame = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a => a.GameId, gameId);
            var item = colRankedGame.Find(filter).First().PlayerOneId;
            if (item != userid)
            {
                money = colRankedGame.Find(filter).First().PlayerOneMoney;
            }
            else
            {
               money = colRankedGame.Find(filter).First().PlayerTwoMoney;
            }
            return money;
        }
    }
}
