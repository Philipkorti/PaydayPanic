using Data;
using DataBase.Context;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDay.ViewModels
{
    public class StatisticViewModel : ViewModelBase
    {
        /// <summary>
        /// Save data for the satatistic.
        /// </summary>
        private StatisticData statisticData;

        /// <summary>
        /// Initialize a new instance of the <see cref="StatisticViewModel"/> class.
        /// </summary>
        /// <param name="game">Game data</param>
        public StatisticViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            using(var context = new PayDayContext())
            {
                var item = (from high in context.Highscore
                            join u in context.User on high.UserID equals u.UserId
                            join r in context.Ranks on high.RankID equals r.Id
                            join s in context.Statistics on u.UserId equals s.UserID
                            join c in context.Casino on s.StatisticID equals c.StatisticsID
                            join sh in context.Shop on s.StatisticID equals sh.StatisticsID
                            where u.UserName == game.Username
                             select new
                             {
                                 u.UserId,
                                 u.UserName,
                                 s.GameCount,
                                 s.GameMoneyWin,
                                 s.GameMoneyLose,
                                 c.WinSeven,
                                 c.WinHeart,
                                 c.WinMoneyBag,
                                 c.CasinoWinCount,
                                 c.GameCasinoCount,
                                 sh.InputMoney,
                                 sh.OutputMoney,
                                 sh.BoughtGamesCount,
                                 sh.GamesSoldCount,
                                 r.Rank,
                                 r.RankURL,
                                 high.Elo

                             }).OrderByDescending(x => x.Elo).Take(20).ToList();
                this.StatisticData = new StatisticData(item[0].Elo, item[0].RankURL, item[0].BoughtGamesCount, item[0].GamesSoldCount, item[0].OutputMoney,
                    item[0].InputMoney, item[0].GameCount, item[0].GameMoneyWin, item[0].GameMoneyLose, item[0].CasinoWinCount, item[0].GameCasinoCount,
                    item[0].WinSeven, item[0].WinMoneyBag, item[0].WinHeart);
                

            }
        }
        /// <summary>
        /// Gets or sets the statistic data.
        /// </summary>
        public StatisticData StatisticData
        {
            get { return statisticData; }
            set 
            { 
                this.statisticData = value; 
                this.OnPropertyChanged(nameof(this.StatisticData));
            }
        }
    }
}
