using Data;
using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayDay.ViewModels
{
    public class HighscoreViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private List<HighscoreViewData> highscore;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public HighscoreViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Highscore= new List<HighscoreViewData>();
            
            using (var context = new PayDayContext())
            {
                
                var item = (from high in context.Highscore
                        join u in context.User on high.UserID equals u.UserId
                        join r in context.Ranks on high.RankID equals r.Id
                        where u.UserId == high.UserID
                        select new
                        {
                            u.UserId,
                            u.UserName,
                            high.HighestScore,
                            r.Rank,
                            r.RankURL,
                            high.Elo
                        }).OrderByDescending(x => x.Elo).Take(20).ToList();

                foreach (var high in item)
                {
                    Highscore.Add(new HighscoreViewData() { UserID = high.UserId, UserName = high.UserName, HighestScore = high.HighestScore, Rank = high.Rank, Elo = high.Elo, RankURL = high.RankURL});               
                }
                
            }
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public List<HighscoreViewData> Highscore
        {
            get { return highscore; }
            set { highscore = value; }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------

        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------

        #endregion
    }
}
