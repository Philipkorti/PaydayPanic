using DataBase.Context;
using DataBase.Models;
using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public static class RegisterServices
    {
        
        public static bool Register(string username, string password) 
        {
            bool check = true;
                using (var context = new PayDayContext())
                {
                    var users = context.User.ToList();
                    foreach (User item in users)
                    {
                        if (item.UserName == username)
                        {
                            check = false;
                            break;
                        }
                    }

                }

            if (check)
            {
                using (var payDay = new PayDayContext())
                {
                    var user = new User() { UserName = username, Password = password };
                    var rank = payDay.Ranks.ToList();
                    var highscore = new Highscore() { UserID = user.UserId, RankID = rank[0].Id};
                    var statistics = new Statistics() { UserID = user.UserId };
                    var casino = new Casino() { StatisticsID = statistics.StatisticID };
                    var shop = new Shop() { StatisticsID = statistics.StatisticID };
                    payDay.User.Add(user);
                    payDay.Highscore.Add(highscore);
                    payDay.Statistics.Add(statistics);
                    payDay.Casino.Add(casino);
                    payDay.Shop.Add(shop);
                    payDay.SaveChanges();
                }
            }
                return check;
        }
    }
}
