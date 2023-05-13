using DataBase.Context;
using DataBase.Models;
using System.Linq;

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
                    var highscore = new Highscore() { UserID = user.UserId, RankID = rank[0].Id };
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

        public static bool LogIn(string username, string password)
        {
            bool isLogin = false;
            using (var context = new PayDayContext())
            {
                var users = context.User.ToList();
                foreach (var user in users)
                {
                    if (username == user.UserName && password == user.Password)
                    {
                        isLogin = true;
                        break;
                    }
                    else
                    {
                        isLogin = false;
                    }
                }

            }
            return isLogin;
        }
    }
}
