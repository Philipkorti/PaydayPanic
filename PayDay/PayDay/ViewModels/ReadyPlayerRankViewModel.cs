using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class ReadyPlayerRankViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private string playerOneName;
        private string playerTwoName;
        private Game game;
        private DispatcherTimer timer;
        private DateTime dateTime1;
        private DateTime dateTime2;
        private string timerText;
        private bool rady;
        private string buttonText;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ReadyPlayerRankViewModel(IEventAggregator eventAggregator, string playerOne, Game game) : base(eventAggregator) 
        {
            this.playerOneName = playerOne;
            this.game = game;
            GetSecondPlayerName();
            this.TimerText = " 3:00";
            this.dateTime1 = new DateTime();
            this.dateTime2 = new DateTime(1,1,1,0,3,0);
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += ReadRady;
            worker.RunWorkerCompleted += ReadRadyEnd;
            worker.RunWorkerAsync();
            this.ButtonText = "Rady";
            this.RadyPlayerCommand = new ActionCommand(this.RadyPlayerExecuted, this.ReadyPlayerCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string PlayerOneName { get { return playerOneName; } }
        public string PlayerTwoName { get { return playerTwoName; } }
        public ICommand RadyPlayerCommand { get; private set; }

        public string TimerText
        {
            get { return this.timerText; }
            set
            {
                if(this.timerText!=value)
                {
                    this.timerText = value;
                    this.OnPropertyChanged(nameof(this.TimerText));
                }
            }
        }

        public string ButtonText
        {
            get { return this.buttonText; }
            set
            {
                if(this.buttonText!=value)
                {
                    this.buttonText = value;
                    this.OnPropertyChanged(nameof(this.ButtonText));
                }
            }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void GetSecondPlayerName()
        {
            string userId = DataBaseRankeGame.ReadRankGameSecondPlayerByGameId(this.game.GameId, this.game.UserId);
            this.playerTwoName = DataBaseRankeGame.ReadRankSecondName(userId);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = this.dateTime2 - this.dateTime1;
            this.dateTime1 = this.dateTime1.AddSeconds(1);
            this.TimerText = timeSpan .ToString(@"mm\:ss");

            if(this.TimerText == "00:00")
            {
                timer.Stop();
            }
        }

        private void ReadRady(object sender, DoWorkEventArgs e)
        {
            bool check;
            do
            {
                check = DataBaseRankeGame.IsRady(this.game.GameId, this.game.UserId);
            } while (!this.rady || !check);
        }

        private void ReadRadyEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            GameView gameView = new GameView();
            GameViewModel gameViewModel = new GameViewModel(this.EventAggregator, this.game);
            gameView.DataContext = gameViewModel;
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter ready player can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwiaw <c>false</c>.</returns>
        private bool ReadyPlayerCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the ready button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RadyPlayerExecuted(object parameter)
        {
            this.rady = this.rady == true ? false: true;
            this.ButtonText = this.rady == true ? "Not Rady" : "Rady";
            DataBaseRankeGame.SetRadyRankedGame(this.game.GameId,this.game.UserId, this.rady);
        }
        #endregion
    }
}
