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
using System.Threading.Tasks;
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
        #endregion

    }
}
