using Common.Command;
using DataBase.Context;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        /// <summary>
        /// Is the login loding.
        /// </summary>
        private bool isLoading;
        /// <summary>
        /// Whether the login was successful.
        /// </summary>
        
        private bool isLogIn;
        /// <summary>
        /// Whether the player wants to save the login.
        /// </summary>
        private bool saveLogIn;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="LogInViewModel"/> class.
        /// </summary>
        public LogInViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            
            RegisterCommand = new ActionCommand(this.RegisterCommandExecuted, this.RegisterCommandCanExecute);
            LogInCommand = new ActionCommand(this.LogInCommandExecute, this.LogInCommandCanExecute);
            this.IsLoading = false;
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
        /// Gets or sets whether the player wants to save the login.
        /// </summary>
        public bool SaveLogIn
        {
            get { return this.saveLogIn; }
            set { this.saveLogIn= value; }
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

        /// <summary>
        /// Gets or sets is the login loding.
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            set 
            { 
                this.isLoading = value;
                this.OnPropertyChanged(nameof(this.IsLoading));
            }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// The login method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogIn(object sender, DoWorkEventArgs e)
        {
            this.IsLoading = true;
           this.isLogIn = RegisterServices.LogIn(this.Username, this.Password);
            if(!isLogIn)
            {
                MessageBox.Show("Der Username oder das Passwort ist falsch!", ErrorCodes.LoginError.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.IsLoading = false;
        }
        /// <summary>
        /// Login was successful method
        /// </summary>
        private void LogInEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isLogIn)
            {
                MainMenu mainMenu = new MainMenu();
                MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator, this.Username);
                mainMenu.DataContext = mainMenuModel;
                this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Publish(mainMenu);
                if (this.SaveLogIn)
                {
                    AutoLogIn.CreateFile(this.Username, this.password);
                }
            }
            
        }
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
            BackgroundWorker backgroundWorker= new BackgroundWorker();
            backgroundWorker.DoWork += LogIn;
            backgroundWorker.RunWorkerCompleted += LogInEnd;
            backgroundWorker.RunWorkerAsync();
            
            
        }
        #endregion
    }
}
