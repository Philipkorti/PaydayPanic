using DataBase.Context;
using DataBase.Models;
using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Data;
using System.Collections.Generic;

namespace Services.Services
{
    public static class RegisterServices
    {

        public static bool Register(string username, string password, out ErrorCodes errorCodes)
        {
            bool check = true;
            errorCodes = new ErrorCodes();
            try
            {
                var productCollection = DataBaseService.GetUserCollection();
                var filter = Builders<User>.Filter.Empty;
                var user = productCollection.Find(filter).ToList();
                foreach ( var item in user )
                {
                    if(item.UserName == username)
                    {
                        check = false;
                        errorCodes = ErrorCodes.RegisterError;
                        break;
                    }
                }

            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
            }

            if (check)
            {
                try
                {
                    var productCollection = DataBaseService.GetUserCollection();
                    User user = new User
                    {
                        UserName = username,
                        Password = password,
                        Statistics = new List<Statistics>
                    {
                        new Statistics
                        {
                            GameCount = 0,
                            GameMoneyLose = 0,
                            GameMoneyWin = 0,
                            Casino = new List<Casino>
                            {
                                new Casino
                                {
                                    CasinoWinCount = 0,
                                    GameCasinoCount = 0,
                                    WinHeart = 0,
                                    WinMoneyBag = 0,
                                    WinSeven = 0
                                }
                            },
                            Shop = new List<Shop>
                            {
                                new Shop
                                {
                                    BoughtGamesCount = 0,
                                    GamesSoldCount = 0,
                                    InputMoney = 0,
                                    OutputMoney = 0,
                                }
                            }
                        },

                    },
                        Highscore = new List<Highscore>
                    {
                        new Highscore
                        {
                            Elo = 0,
                            Rank = ConstData.Bronze
                        }
                    }
                    };
                    productCollection.InsertOne(user);
                    check = true;
                }
                catch (Exception)
                {
                    errorCodes = ErrorCodes.RegisterError;
                    check = false;
                }
            }
            return check;
        }

        public static bool LogIn(string username, string password, out ErrorCodes errorCodes, out string userId)
        {
            bool isLogin = false;
            errorCodes = new ErrorCodes();
            userId = null;
            try
            {

                var productCollection = DataBaseService.GetUserCollection();
                var filter = Builders<User>.Filter.Empty;
                var users = productCollection.Find(filter).ToList();
                foreach (var user in users)
                {
                    if (username == user.UserName && password == user.Password)
                    {
                        isLogin = true;
                        errorCodes = ErrorCodes.NoError;
                        userId = user.UserId;
                        break;
                    }
                    else
                    {
                       isLogin = false;
                       errorCodes = ErrorCodes.LoginError;
                    }
                }
            }
            catch (Exception)
            {
                errorCodes = ErrorCodes.DBSCon2202;
                isLogin = false;
            }
            
            return isLogin;
        }
    }
}
