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
using System.Windows.Controls;
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
        private string radyTextPlayerOne;
        private string radyTextPlayerTwo;
        private string countownText;
        private UserControl userControl;
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
            TextChatView textChatView = new TextChatView(this.game.UserId,this.game.GameId);
            TextChatViewModel textChatViewModel = new TextChatViewModel(this.EventAggregator,this.game);
            textChatView.DataContext = textChatViewModel;
            this.UserControl = textChatView;
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

        public UserControl UserControl
        {
            get { return this.userControl; }
            set
            {
                if(this.userControl != value)
                {
                    this.userControl = value;
                    this.OnPropertyChanged(nameof(this.UserControl));
                }
            }
        }

        public string RadyTextPlayerOne
        {
            get { return this.radyTextPlayerOne;}
            set
            {
                if (this.radyTextPlayerOne != value)
                {
                    this.radyTextPlayerOne= value;
                    this.OnPropertyChanged(nameof(this.RadyTextPlayerOne));
                }
            }
        }

        public string RadyTextPlayerTwo
        {
            get { return this.radyTextPlayerTwo;}
            set
            {
                if(this.radyTextPlayerTwo != value)
                {
                    this.radyTextPlayerTwo= value;
                    this.OnPropertyChanged(nameof(this.RadyTextPlayerTwo));
                }
            }
        }

        public string CountownText
        {
            get { return this.countownText; }
            set
            {
                if(this.CountownText != value)
                {
                    this.countownText= value;
                    this.OnPropertyChanged(nameof(this.CountownText));
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
            TimeSpan time = TimeSpan.FromSeconds(3);
            this.dateTime1 = this.dateTime1.AddSeconds(1);
            this.TimerText = timeSpan .ToString(@"mm\:ss");
            if (timeSpan <= time)
            {
                this.CountownText = timeSpan.ToString("ss");
            }
            if(this.TimerText == "00:00")
            {
                timer.Stop();
                if (this.rady && this.RadyTextPlayerTwo == "Rady")
                {
                    GameView gameView = new GameView();
                    GameViewModel gameViewModel = new GameViewModel(this.EventAggregator, this.game);
                    gameView.DataContext = gameViewModel;
                    this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
                }
                else
                {
                    DataBaseRankeGame.DeletRankGame(this.game.GameId);
                    MainMenu mainMenu = new MainMenu();
                    MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator, this.game.Username, this.game.UserId);
                    mainMenu.DataContext = mainMenuModel;
                    this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Publish(mainMenu);
                }
               
            }
        }

        private void ReadRady(object sender, DoWorkEventArgs e)
        {
            bool check;
            do
            {
                check = DataBaseRankeGame.IsRady(this.game.GameId, this.game.UserId);
                this.RadyTextPlayerOne = this.rady == true ? "Rady" : "Not Rady";
                this.RadyTextPlayerTwo = check == true ? "Rady" : "Not Rady";
            } while (!this.rady || !check);
        }

        private void ReadRadyEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dateTime2 = new DateTime(1, 1, 1, 0, 0, 20);
            this.dateTime1 = new DateTime(1, 1, 1, 0, 0, 0);
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
            if (this.rady && this.RadyTextPlayerTwo == "Rady" && this.TimerText == "00:10")
            {
                return false;
            }
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
