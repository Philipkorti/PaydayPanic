using Data;
using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SearchPlayerService
    {
        private static List<User> getWaitingUser()
        {
            var waitinglist = DataBaseService.GetWaitingListCollection();
            var filter = Builders<WaitingList>.Filter.Empty;
            var waitingLists = waitinglist.Find(filter).ToList();
            var filterDes = new FilterDefinitionBuilder<User>();
            List<string> waitingIds = new List<string>();
           foreach (var waiting in waitingLists)
            {
                waitingIds.Add(waiting.UserId);
            }
            var filter1 = filterDes.In(x => x.UserId,waitingIds);
            var userColection = DataBaseService.GetUserCollection();
            var user = userColection.Find(filter1).ToList();
            return user;
        }

        public static void SearchPlayer(ref Game game)
        {
            var user = getWaitingUser();
            string player = getPlayer(game, user);
            game.GameId = DataBaseRankeGame.SetRankGame(game.UserId, player);
        }

        private static string getPlayer(Game game, List<User> user)
        {
            string player = null;
            Int64 rankpointsdiff = 2;
            int count;
            do
            {
                foreach(var item in user)
                {
                    count = Math.Abs(item.Highscore[0].Elo - game.Highscore);
                    
                    if (count <= rankpointsdiff && item.UserId != game.UserId)
                    {
                        player = item.UserId; 
                        break;
                    }
                }
                user = getWaitingUser();
                if (rankpointsdiff< int.MaxValue)
                {
                    rankpointsdiff = rankpointsdiff * 2;
                }
                


            } while (player == null);
            return player;
        }
    }
}
