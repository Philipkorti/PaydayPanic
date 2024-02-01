using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class SelectGameViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// Instance of the game class.
        /// </summary>
        public Game Game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public SelectGameViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator) 
        {
            this.Game = game;
            this.SingelPlayerCommand = new ActionCommand(this.SingelplayerCommandExecute, this.SingelPlayerCommandCanExecute);
            this.RankedMultiplayerCommand = new ActionCommand(this.MultiplayerCommandExecute, this.MultiplayerCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand SingelPlayerCommand
        {
            get; private set;
        }
        public ICommand RankedMultiplayerCommand
        {
            get; private set;
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines whether the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SingelPlayerCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the Singelplayer button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SingelplayerCommandExecute(object parameter)
        {
            DataBaseService.PlusGame(this.Game);
            GameView gameView = new GameView();
            GameViewModel gameViewModel = new GameViewModel(this.EventAggregator,this.Game);
            gameView.DataContext = gameViewModel;
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
        }

        /// <summary>
        /// De´termines whether the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool MultiplayerCommandCanExecute(object parameter)
        {
            return true;
        }

        private void MultiplayerCommandExecute(object parameter)
        {
            
            if (WaitingListService.SetWaitingList(this.Game.Username, out ErrorCodes errorCodes))
            {
                ErrorServices.ShowError(errorCodes);
            }
            else
            {
                DataBaseService.PlusGame(this.Game);
                RankedMultiplayerWaitingView rankedMultiplayerWaitingView = new RankedMultiplayerWaitingView();
                RankedMultiplayerWaitingViewModel rankedMultiplayerWaitingViewModel = new RankedMultiplayerWaitingViewModel(this.EventAggregator);
                rankedMultiplayerWaitingView.DataContext = rankedMultiplayerWaitingViewModel;
                this.EventAggregator.GetEvent<RankedMultiplayerWaitingViewDataChangeEvent>().Publish(rankedMultiplayerWaitingView);
            }
           
            
        }

        #endregion

    }
}
