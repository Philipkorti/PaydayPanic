using Common.Command;
using DataBase.Context;
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
using System.Windows.Documents;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private string username;
        private string password;
        private string errortext;
        public ICommand LogInCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public LogInViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            RegisterCommand = new ActionCommand(this.RegisterCommandExecuted, this.RegisterCommandCanExecute);
            LogInCommand = new ActionCommand(this.LogInCommandExecute, this.LogInCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        

        public string Username
        {
            get 
            { 
            return username;
            }
            set
            {
                if(username != value)
                {
                    username = value;
                }
            }
        }
        public string Password
        {
            private get { return password;}
            set
            {
                if(password != value)
                {
                    password = SHA.ComputeSha256Hash(value);
                }
            }
        }

        public string ErrorText
        {
            get { return errortext; }
            set
            {
                if(errortext != value)
                {
                    errortext = value;
                    this.OnPropertyChanged(nameof(ErrorText));
                }
            }
        }
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

        private bool LogInCommandCanExecute(object parameter)
        {
            return true;
        }
        private void LogInCommandExecute(object parameter)
        {
            using(var context = new PayDayContext())
            {
                var users = context.Users.ToList();
                foreach(var user in users)
                {
                    if(this.Username == user.UserName && this.Password == user.Password)
                    {
                        MainMenu mainMenu = new MainMenu();
                        MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator);
                        mainMenu.DataContext = mainMenuModel;
                        this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Publish(mainMenu);
                    }
                    else
                    {
                        MessageBox.Show("Der Username oder das Passwort ist falsch!",ErrorCodes.LoginError.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion
    }
}
