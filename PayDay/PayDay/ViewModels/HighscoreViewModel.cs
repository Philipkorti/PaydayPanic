using Data;
using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using Services.Services;
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
        /// <summary>
        /// List for saving the best players.
        /// </summary>
        private List<HighscoreViewData> highscore;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="HighscoreViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"></param>
        public HighscoreViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Highscore= new List<HighscoreViewData>();
            DataBaseService.LoadHighscore(Highscore);
           
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets or sets the player of the highscore.
        /// </summary>
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
