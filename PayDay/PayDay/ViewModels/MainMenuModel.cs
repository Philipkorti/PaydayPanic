using Common.Command;
using PayDay.Events;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Data;
using System.Windows.Controls;

namespace PayDay.ViewModels
{
    public class MainMenuModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// Instance of the game class.
        /// </summary>
        public Game Game;

        /// <summary>
        /// View that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        private UserControl viewLeft;
        private RulesView rulesView;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="MainMenuModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="username"></param>
        public MainMenuModel(IEventAggregator eventAggregator, string username) : base(eventAggregator)
        {
            this.ExitCommand = new ActionCommand(this.ExitCommandExecute, this.ExitCommandCanExecute);
            this.PlayCommand = new ActionCommand(this.PlayCommandExecute, this.PlayCommandCanExecute);
            this.RulleCommand = new ActionCommand(this.RullesCommandExecute, this.RullesCommandCanExecute);
            Game = new Game(1000, username, 0,1,1);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(Game);
            rulesView = new RulesView();
            RulesViewModel rulesViewModel = new RulesViewModel(this.EventAggregator);
            rulesView.DataContext = rulesViewModel;
            
        }

        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets the exit button command.
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the play button command.
        /// </summary>
        public ICommand PlayCommand { get; private set; }

        /// <summary>
        /// Gets the rulle button command.
        /// </summary>
        public ICommand RulleCommand { get; private set; }

        /// <summary>
        /// Gets the TopPlayer button command.
        /// </summary>
        public ICommand TopPlayerCommand { get; private set; }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        public UserControl ViewLeft
        {
            get { return viewLeft; }
            set 
            {
                if(viewLeft != value)
                {
                    viewLeft = value;
                    this.OnPropertyChanged(nameof(ViewLeft));
                }
                
            }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------

        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the exit command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ExitCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the exit button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void ExitCommandExecute(object parameter)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Determines wheter the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool PlayCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the play button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void PlayCommandExecute(object parameter)
        {
            GameView gameView = new GameView();
            GameViewModel gameViewModel = new GameViewModel(this.EventAggregator, this.Game);
            gameView.DataContext = gameViewModel;
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
        }

        /// <summary>
        /// Determines wheter the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool RullesCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the rulle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RullesCommandExecute(object parameter)
        {
            this.ViewLeft = this.ViewLeft == null ? this.rulesView : null;
        }
        #endregion
    }
}
