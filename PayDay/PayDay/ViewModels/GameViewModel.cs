﻿using Common.Command;
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
using System.Windows.Input;
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
        private GameBordView gameBordView;

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
            this.BackMapCommand = new ActionCommand(this.BackCommandExecuted, this.BackCommandCanExecute);
            gameBordView = new GameBordView();
            GameBordViewModel gameBordViewModel = new GameBordViewModel(this.EventAggregator, game);
            gameBordView.DataContext= gameBordViewModel;
            this.CurrentView = gameBordView;
            this.EventAggregator.GetEvent<CsinoViewDataChangeEvent>().Subscribe(this.OnCasinoViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<ShopViewDataChangeEvent>().Subscribe(this.OnShopViewChanged, ThreadOption.UIThread);
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
        /// <summary>
        /// Get the backMapCommand button command.
        /// </summary>
        public ICommand BackMapCommand { get; private set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void TimerTick(object sender, EventArgs e)
        {
            datetime = datetime.AddMinutes(1);
            TimerText = datetime.ToString("HH:mm");
        }
        private void OnCasinoViewChanged(CasinoView casinoView)
        {
            this.CurrentView = casinoView;
        }
        private void OnGameDataChanged(Game game)
        {
            this.Game = game;
        }

        private void OnShopViewChanged(ShopView shopView)
        {
            this.CurrentView = shopView;
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
            return this.CurrentView != gameBordView ? true : false;
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
