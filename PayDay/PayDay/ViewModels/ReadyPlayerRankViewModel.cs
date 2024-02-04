using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class ReadyPlayerRankViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private string playerOneName;
        private string playerTwoName;
        private Game game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ReadyPlayerRankViewModel(IEventAggregator eventAggregator, string playerOne, Game game) : base(eventAggregator) 
        {
            this.playerOneName = playerOne;
            this.game = game;
            GetSecondPlayerName();
            this.RadyPlayerCommand = new ActionCommand(this.RadyPlayerExecuted, this.ReadyPlayerCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string PlayerOneName { get { return playerOneName; } }
        public string PlayerTwoName { get { return playerTwoName; } }
        public ICommand RadyPlayerCommand { get; private set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void GetSecondPlayerName()
        {
            string userId = DataBaseRankeGame.ReadRankGameSecondPlayerByGameId(this.game.GameId, this.game.UserId);
            this.playerTwoName = DataBaseRankeGame.ReadRankSecondName(userId);
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
            DataBaseRankeGame.SetRadyRankedGame(this.game.GameId,this.game.UserId);
        }
        #endregion
    }
}
