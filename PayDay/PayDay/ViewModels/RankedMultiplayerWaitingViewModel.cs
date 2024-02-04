using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class RankedMultiplayerWaitingViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private DateTime timeSpan;
        DispatcherTimer timer;
        private string timertext;
        Game game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public RankedMultiplayerWaitingViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            this.game = game;
            this.timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += this.Timer;
            timer.Start();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += SelectPerson;
            worker.RunWorkerCompleted += SelectPersonEnd;
            worker.RunWorkerAsync();
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string Timertext
        {
            get { return this.timertext; }
            set 
            {
                if (this.Timertext != value)
                {
                    this.timertext = value;
                    this.OnPropertyChanged(nameof(this.Timertext));
                }
            }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void Timer(object sender, EventArgs e)
        {
            this.timeSpan = this.timeSpan.AddSeconds(1);
            this.Timertext = this.timeSpan.ToString("mm:ss");
        }

        private void SelectPerson(object sender, DoWorkEventArgs e)
        {
            SearchPlayerService.SearchPlayer(ref this.game);
        }

        private void SelectPersonEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            this.timer.Stop();
            if (!WaitingListService.DeleteWaitingList(this.game.UserId, out ErrorCodes errorCodes))
            {
                ReadyPlayerRankViewModel readyPlayerRankViewModel = new ReadyPlayerRankViewModel(this.EventAggregator, this.game.Username,this.game);
                ReadyPlayerRankView readyPlayerRankView = new ReadyPlayerRankView();
                readyPlayerRankView.DataContext = readyPlayerRankViewModel;
                this.EventAggregator.GetEvent<ReadyPlayerRankDataChangeEvent>().Publish(readyPlayerRankView);
            }
            else
            {
                ErrorServices.ShowError(errorCodes);
            }
            
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        #endregion
    }
}
