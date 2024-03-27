using Data;
using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Services.Services
{
    public class TextChatService
    {
        public static void SetTextChat(string userId, string textUser,string gameId)
        {
            var col = DataBaseService.GetTextChatCollection();
            TextChat text = new TextChat()
            {
                PlayerID = userId,
                Text = textUser,
                GameId = gameId
            };
            col.InsertOne(text);
        }

        public static List<Chat> GetTextChatMessages(string gameid, string userid, string username) 
        {
            var col = DataBaseService.GetTextChatCollection();
            var filter = Builders<TextChat>.Filter.Eq(a=>a.GameId,gameid);
            var items = col.Find(filter).ToList();
            List<Chat> messages = new List<Chat>();
            string secondplayerID = DataBaseRankeGame.ReadRankGameSecondPlayerByGameId(gameid, userid);
            string secondplayer = DataBaseRankeGame.ReadRankSecondName(secondplayerID);
            try
            {
                foreach (var item in items)
                {
                    if (item.PlayerID == userid)
                    {
                        messages.Add(new Chat() { TextPLayerOne = username +": " + item.Text });
                    }
                    else
                    {
                        messages.Add(new Chat() { TextPLayerTwo =secondplayer +": " + item.Text });
                    }
                }
            }catch(Exception ex)
            {

            }
           
            return messages;
        }

        public static void DeleteMessages(string rankGameId)
        {
            var colMessages = DataBaseService.GetTextChatCollection();
            var filter = Builders<TextChat>.Filter.Eq(a => a.GameId, rankGameId);
            colMessages.DeleteMany(filter);
        }
    }
}
