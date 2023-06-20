using Data;
using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
using Services;
using Services.Services;
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
            DataBaseService.LoadStatistic(out statisticData, out ErrorCodes errorCodes, game);
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
