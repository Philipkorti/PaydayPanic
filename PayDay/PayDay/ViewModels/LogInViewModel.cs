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
        /// <summary>
        /// Username of the player.
        /// </summary>
        private string username;

        /// <summary>
        /// Password of the player.
        /// </summary>
        private string password;

        /// <summary>
        /// Error Text if someting goes wrong.
        /// </summary>
        private string errortext;

        /// <summary>
        /// Gets the login button command.
        /// </summary>
        public ICommand LogInCommand { get; private set; }

        /// <summary>
        /// Gets the register button command.
        /// </summary>
        public ICommand RegisterCommand { get; private set; }
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="LogInViewModel"/> class.
        /// </summary>
        public LogInViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            RegisterCommand = new ActionCommand(this.RegisterCommandExecuted, this.RegisterCommandCanExecute);
            LogInCommand = new ActionCommand(this.LogInCommandExecute, this.LogInCommandCanExecute);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        
        /// <summary>
        /// Gets and sets the username of the player.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the password of the player.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the errorText.
        /// </summary>
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
        /// <summary>
        /// Determines wheter the register command be executed.
        /// </summary>
        /// <param name="parameter">Sata used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool RegisterCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the register button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RegisterCommandExecuted(object parameter)
        {
            SignInView signInView = new SignInView();
            SignInViewModel signInViewModel = new SignInViewModel(this.EventAggregator);
            signInView.DataContext = signInViewModel;
            this.EventAggregator.GetEvent<RegisterDataChageEvent>().Publish(signInView);
        }

        /// <summary>
        /// Determines wheter the login command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool LogInCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the login button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
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
                        MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator, this.Username);
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
