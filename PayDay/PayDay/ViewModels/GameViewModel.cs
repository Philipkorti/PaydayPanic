using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

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
        /// DateTime to save the playtime.
        /// </summary>
        private DateTime datetime;

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
       
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the game class.</param>
        public GameViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            TimerText = "00:00";
            time = new DispatcherTimer();
            datetime= new DateTime();
            time.Interval = TimeSpan.FromMinutes(1);
            time.Tick += TimerTick;
            time.Start();
            GameBordView gameBordView = new GameBordView();
            GameBordViewModel gameBordViewModel = new GameBordViewModel(this.EventAggregator, game);
            gameBordView.DataContext= gameBordViewModel;
            this.CurrentView = gameBordView;
            this.EventAggregator.GetEvent<CsinoViewDataChangeEvent>().Subscribe(this.OnCasinooViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChanged, ThreadOption.UIThread);
            this.Game = game;
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
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void TimerTick(object sender, EventArgs e)
        {
            datetime = datetime.AddMinutes(1);
            TimerText = datetime.ToString("HH:mm");
        }
        private void OnCasinooViewChanged(CasinoView casinoView)
        {
            this.CurrentView = casinoView;
        }
        private void OnGameDataChanged(Game game)
        {
            this.Game = game;
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------

        #endregion
    }
}
