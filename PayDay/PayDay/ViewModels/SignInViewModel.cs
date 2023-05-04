using Common.Command;
using Data;
using DataBase;
using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
using Microsoft.Win32;
using Services;
using Services.Enums;
using Services.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class SignInViewModel : ViewModelBase
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
        /// Length of the username.
        /// </summary>
        private int usernameCount;

        /// <summary>
        /// Max username length.
        /// </summary>
        private int userLength;

        private bool isLoading;

        /// <summary>
        /// Gets the signin button command.
        /// </summary>
        public ICommand SignIn { get; private set; }
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="SignInViewModel"/> class.
        /// </summary>
        public SignInViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SignIn = new ActionCommand(this.SignInCommandExecute, this.SignInCommandCanExecute);
            UsernameCount= 0;
            UserLength = ConstData.StringLengh;
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets and sets the username of the player.
        /// </summary>
        public string Username
        {
            get { return username; }
            set
            {
                UsernameCount = value.Length;
                if (this.username != value && UsernameCount <= UserLength)
                {
                    username = value;
                }
                else
                {
                    UsernameCount--;
                }
                
            }
        }

        /// <summary>
        /// Gets and sets the password of the player.
        /// </summary>
        public string Password
        {
            set
            {
                password = SHA.ComputeSha256Hash(value);
            }
        }

        /// <summary>
        /// Gets and sets usernameCount.
        /// </summary>
        public int UsernameCount
        {
            get { return usernameCount; }
            set 
            { 
                this.usernameCount = value;
                this.OnPropertyChanged(nameof(UsernameCount));
            }
        }

        /// <summary>
        /// Gets and sets userLength.
        /// </summary>
        public int UserLength
        {
            get { return this.userLength; }
            set { this.userLength = value; }
        }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                if (this.isLoading != value)
                {
                    this.isLoading = value;
                    this.OnPropertyChanged(nameof(IsLoading));
                }
            }
        }
        #endregion



       #region ------------------------- Private helper ------------------------------------------------------------------
       private void DataBaseConect()
        {
            IsLoading = true;
            bool check = true;
            try
            {
                check = RegisterServices.Register(this.Username, this.password);
            }catch(Exception ex)
            {
                IsLoading= false;
                MessageBox.Show(ErrorCodes.DBSCon2202 + "Contection ERROR", ErrorCodes.DBSCon2202.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(check)
            {
                IsLoading = false;
                MessageBox.Show("Welcome to PayDay Panic!", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            
        }
        #endregion
       

           #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the students view command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SignInCommandCanExecute(object parameter)
        {
            if (this.Username != null && this.password != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Ocures when the user clicks the signIn button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SignInCommandExecute(object parameter)
        {
            
            Thread thread = new Thread(DataBaseConect);
            thread.Start();
            
        }
        #endregion
    }
}
