using Common.Command;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public LogInViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            RegisterCommand = new ActionCommand(this.RegisterCommandExecuted, this.RegisterCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand RegisterCommand { get; private set; }
        public ICommand LogInCommand { get; private set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------

        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        private bool RegisterCommandCanExecute(object parameter)
        {
            return true;
        }
        private void RegisterCommandExecuted(object parameter)
        {
            SignInView signInView = new SignInView();
            SignInViewModel signInViewModel = new SignInViewModel(this.EventAggregator);
            signInView.DataContext = signInViewModel;
            this.EventAggregator.GetEvent<RegisterDataChageEvent>().Publish(signInView);
        }
        #endregion
    }
}
