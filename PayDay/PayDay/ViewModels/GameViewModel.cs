using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WMPLib;

namespace PayDay.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// A second Thred to create a play time.
        /// </summary>
        private DispatcherTimer time;

        /// <summary>
        /// The maximum game time.
        /// </summary>
        private DateTime datetime;

        /// <summary>
        /// The rest of the game time.
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// timeText is for the output the play time.
        /// </summary>
        private string timerText;

        /// <summary>
        /// View that is curenrly bound to the <see cref="ContentControl"/>.
        /// </summary>
        private UserControl currentView;

        /// <summary>
        /// Instance of the game class.
        /// </summary>
        private Game game;

        /// <summary>
        /// Instance of the gamebordview userconrol.
        /// </summary>
        private GameBordView gameBordView;

        /// <summary>
        /// Whether the home button can be active.
        /// </summary>
        private bool backButton;
        private WindowsMediaPlayer mediaPlayer;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the game class.</param>
        public GameViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            mediaPlayer= new WindowsMediaPlayer();
            this.Game = game;
            TimerText = "10:00";
            this.backButton= true;
            this.dateTime = new DateTime();
            time = new DispatcherTimer();
            datetime= new DateTime(1,1,1,0,1,0);
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += TimerTick;
            time.Start();
            this.BackMapCommand = new ActionCommand(this.BackCommandExecuted, this.BackCommandCanExecute);
            gameBordView = new GameBordView();
            GameBordViewModel gameBordViewModel = new GameBordViewModel(this.EventAggregator, game);
            gameBordView.DataContext= gameBordViewModel;
            this.CurrentView = gameBordView;
            this.EventAggregator.GetEvent<CsinoViewDataChangeEvent>().Subscribe(this.OnCasinoViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<ShopViewDataChangeEvent>().Subscribe(this.OnShopViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Subscribe(this.OnbackCommandChange, ThreadOption.UIThread);
            this.mediaPlayer.URL = "F:\\Entwicklung\\PaydayPanic\\PayDay\\PayDay\\Music\\Ingame.mp3";
            this.mediaPlayer.settings.setMode("loop",true);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets and sets the timerText.
        /// </summary>
        public string TimerText
        {
            get { return timerText; }
            set { 
                if(timerText!= value)
                {
                    timerText = value;
                    this.OnPropertyChanged(nameof(TimerText));
                }
                
            }
        }
        
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="ContentControl"/>.
        /// </summary>
        public UserControl CurrentView
        {
            get { return currentView; }
            set
            {
                if(currentView!= value)
                {
                    currentView = value;
                    this.OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        /// <summary>
        /// Gets and sets the game class.
        /// </summary>
        public Game Game
        {
            get { return game; }
            set
            {
                game = value;
                this.OnPropertyChanged(nameof(Game));
            }
        }
        /// <summary>
        /// Get the backMapCommand button command.
        /// </summary>
        public ICommand BackMapCommand { get; private set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// Method for the game timer.
        /// </summary>
        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = datetime - dateTime;
            dateTime = dateTime.AddSeconds(1);
            this.TimerText = timeSpan.ToString(@"mm\:ss");
            if (this.TimerText == "01:00")
            {
                this.mediaPlayer.controls.stop();
                this.mediaPlayer.URL = "F:\\Entwicklung\\PaydayPanic\\PayDay\\PayDay\\Music\\lastminute.mp3";
                this.mediaPlayer.controls.play();
            }
            if(this.TimerText == "00:00")
            {
                this.mediaPlayer.controls.stop();
                time.Stop();
                if (this.Game.GameId !=null)
                {
                    DataBaseRankeGame.SetFinishTrue(this.Game.GameId, this.Game.UserId);
                    WaitingEndSecondPlayerView waitingEndSecondPlayerView = new WaitingEndSecondPlayerView();
                    WaitingEndSecondPlayerViewModel waitingEndSecondPlayerViewModel = new WaitingEndSecondPlayerViewModel(this.EventAggregator, this.game);
                    waitingEndSecondPlayerView.DataContext = waitingEndSecondPlayerViewModel;
                    this.EventAggregator.GetEvent<WaitingSecondPersonGameEndDataChangeEvent>().Publish(waitingEndSecondPlayerView);
                }
                else
                {
                    GameEndView gameEndView = new GameEndView();
                    GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator, game);
                    gameEndView.DataContext = gameEndViewModel;
                    this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
                }
                this.Game.GameEndTime = true;
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                
            }
            
        }

        private void OnCasinoViewChanged(CasinoView casinoView)
        {
            this.CurrentView = casinoView;
        }

        private void OnGameDataChanged(Game game)
        {
            this.Game = game;
            if (this.Game.Money < 0)
            {
                GameEndView gameEndView = new GameEndView();
                GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator, this.Game);
                gameEndView.DataContext = gameEndViewModel;
                this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
            }
        }

        private void OnShopViewChanged(L shopView)
        {
            this.CurrentView = shopView;
        }
        
        private void OnbackCommandChange(bool back)
        {
            this.backButton = back;
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the rolle slot machine can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command. </param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool BackCommandCanExecute(Object parameter)
        {
            if (this.backButton)
            {
                return this.CurrentView != gameBordView ? true : false;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Ocures when the user clicks the back button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void BackCommandExecuted(Object parameter)
        {
            this.CurrentView = gameBordView;
        }
        #endregion
    }
}
