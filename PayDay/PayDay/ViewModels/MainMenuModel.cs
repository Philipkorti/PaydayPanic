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

namespace PayDay.ViewModels
{
    public class MainMenuModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------

        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public MainMenuModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.ExitCommand = new ActionCommand(this.ExitCommandExecute, this.ExitCommandCanExecute);
            this.PlayCommand = new ActionCommand(this.PlayCommandExecute, this.PlayCommandCanExecute);
        }

        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand ExitCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }

        #endregion



        #region ------------------------- Private helper ------------------------------------------------------------------

        #endregion



        #region ------------------------- Commands ------------------------------------------------------------------------
        private bool ExitCommandCanExecute(object parameter)
        {
            return true;
        }

        private void ExitCommandExecute(object parameter)
        {
            Application.Current.Shutdown();
        }

        private bool PlayCommandCanExecute(object parameter)
        {
            return true;
        }
        private void PlayCommandExecute(object parameter)
        {
            GameView gameView = new GameView();
            GameViewModel gameViewModel = new GameViewModel(this.EventAggregator);
            gameView.DataContext = gameViewModel;
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
        }
        #endregion
    }
}
