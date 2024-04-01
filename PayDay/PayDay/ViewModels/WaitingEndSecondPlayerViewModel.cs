using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using Services.Services;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using PayDay.Views;

namespace PayDay.ViewModels
{
    public class WaitingEndSecondPlayerViewModel:ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private Game game;
        private string userNameOne;
        private string userNameTwo;
        private double userNameOneMoney;
        private double userNameTwoMoney;
        private string winingText;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public WaitingEndSecondPlayerViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            this.game = game;
            this.game.GameEndTime = true;
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.game);
            this.UserNameOne = this.game.Username;
            this.UserNameOneMoney = this.game.Money;
            string userid;
            this.WiningText = "Waiting...";
            this.NextCommand = new ActionCommand(this.NextCommandExecute, this.NextCommandCanExecute);
            DataBaseRankeGame.SetPlayerMoney(this.game.GameId,this.game.UserId,this.game.Money);
            userid = DataBaseRankeGame.ReadRankGameSecondPlayerByGameId(this.game.GameId,this.game.UserId);
            this.UserNameTwo = DataBaseRankeGame.ReadRankSecondName(userid);
            BackgroundWorker backgroundWorker= new BackgroundWorker();
            backgroundWorker.DoWork += SetFinsihSecondPlayer;
            backgroundWorker.RunWorkerCompleted += Finish;
            backgroundWorker.RunWorkerAsync();
        } 
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------

       public ICommand NextCommand { get; private set; }
       public string UserNameOne
        {
            get
            {
                return this.userNameOne;
            }
            set
            {
                if(this.userNameOne != value)
                {
                    this.userNameOne = value;
                    this.OnPropertyChanged(nameof(this.UserNameOne));
                }
            }
        }

        public string UserNameTwo
        {
            get
            {
                return this.userNameTwo;
            }
            set
            {
                if(this.userNameTwo != value)
                {
                    this.userNameTwo = value;
                    this.OnPropertyChanged(nameof(this.UserNameTwo));
                }
            }
        }

        public string WiningText
        {
            get { return this.winingText; }
            set 
            {
                if(this.winingText != value) 
                { 
                    this.winingText = value;
                    this.OnPropertyChanged(nameof(this.WiningText));
                }
            }
        }

        public double UserNameOneMoney
        {
            get
            {
                return this.userNameOneMoney;
            }
            set
            {
                if(this.userNameOneMoney!= value)
                {
                    this.userNameOneMoney = value;
                    this.OnPropertyChanged(nameof(this.UserNameOneMoney));
                }
            }
        }

       public double UserNameTwoMoney
        {
            get { return this.userNameTwoMoney;}
            set
            {
                if(this.userNameTwoMoney!= value)
                {
                    this.userNameTwoMoney = value;
                    this.OnPropertyChanged(nameof(this.UserNameTwoMoney));
                }
            }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void SetFinsihSecondPlayer(object sender, DoWorkEventArgs args)
        {
            bool finish = false;
            do
            {
                finish = DataBaseRankeGame.ReadSecondPlayerFinish(this.game.GameId, this.game.UserId);
                if (finish)
                {
                    Thread.Sleep(100);
                    this.UserNameTwoMoney = DataBaseRankeGame.ReadSecondPlayerMoney(this.game.GameId,this.game.UserId);
                }

            } while (!finish);
        }

        private void Finish(object sender, RunWorkerCompletedEventArgs args)
        {
            if (this.userNameOneMoney>this.UserNameTwoMoney)
            {
                this.WiningText = "Victury!";
                this.game.Win = true;
                
            }
            else
            {
                this.WiningText = "Lost the game!";
            }
        }
       
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines whether the exit command can be executed.
        /// </summary>
        /// <param name="sender">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
       private bool NextCommandCanExecute(object sender)
        {
            return true;
        }
        /// <summary>
        /// Ocures when the user clicks the Next button.
        /// </summary>
        /// <param name="sender">Data use by the command.</param>
        private void NextCommandExecute(object sender)
        {
            DataBaseRankeGame.DeletRankGame(this.game.GameId);
            GameEndView gameEndView  = new GameEndView();
            GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator, this.game);
            gameEndView.DataContext= gameEndViewModel;
            this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
        }
        #endregion
    }
}
