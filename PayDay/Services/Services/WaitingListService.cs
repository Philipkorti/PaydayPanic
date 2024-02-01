using DataBase.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class WaitingListService
    {
        public static bool SetWaitingList(string username, out ErrorCodes errorCodes)
        {
            bool check = false;
            errorCodes= new ErrorCodes();
            try
            {
                var userCollection = DataBaseService.GetUserCollection();
                var waitinglistCollection = DataBaseService.GetWaitingListCollection();
                var filter = Builders<User>.Filter.Eq(a => a.UserName, username);
                User user = userCollection.Find(filter).First();
                WaitingList waitingList = new WaitingList() {UserId = user.UserId };
                waitinglistCollection.InsertOne(waitingList);
            }catch(Exception ex)
            {
                errorCodes = ErrorCodes.DBSCon2202;
                check= true;
            }

            return check;
        }
    }
}
