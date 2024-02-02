using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DataBaseRankeGame
    {
        public static string SetRankGame(string playerOneId, string playerTwoId)
        {
            var rankedCollection = DataBaseService.GetRankedGameCollection();
            RankGame rankGame = new RankGame
            {
                PlayerOneId = playerOneId,
                PlayerTwoId = playerTwoId,
                PlayerOneRady = false,
                PlayerTwoRady = false
            };
            rankedCollection.InsertOne(rankGame);
            var filter = Builders<RankGame>.Filter.Eq(a=>a.PlayerOneId, playerOneId);
            return rankedCollection.Find(filter).First().GameId;
        }

        public static string ReadRankGameSecondPlayer(string userId)
        {
            var rankeedCollection = DataBaseService.GetRankedGameCollection();
            var filter = Builders<RankGame>.Filter.Eq(a=>a.PlayerTwoId, userId);
            return rankeedCollection.Find(filter).First().PlayerTwoId;
        }
    }
}
