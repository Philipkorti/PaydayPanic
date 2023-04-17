using Common.Command;
using Data;
using DataBase;
using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
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
        private string username;
        private string password;
        private int usernameCount;
        private int userLength;
        private bool isLoading;
        public ICommand SignIn { get; private set; }
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public SignInViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SignIn = new ActionCommand(this.SignInCommandExecute, this.SignInCommandCanExecute);
            UsernameCount= 0;
            UserLength = ConstData.StringLengh;
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
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
        public string Password
        {
            set
            {
                password = SHA.ComputeSha256Hash(value);
            }
        }
        public int UsernameCount
        {
            get { return usernameCount; }
            set 
            { 
                this.usernameCount = value;
                this.OnPropertyChanged(nameof(UsernameCount));
            }
        }

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
                using (var context = new PayDayContext())
                {
                    var users = context.Users.ToList();
                    foreach (User item in users)
                    {
                        if (item.UserName == this.Username)
                        {
                            check = false;
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ErrorCodes.DBSCon2202.ToString());
                check = false;
            }


            if (check)
            {
                try
                {
                    using (var payDay = new PayDayContext())
                    {
                        var user = new User() { UserName = this.Username, Password = password, Elo = Convert.ToString(Ranks.Bronze), highscore = 0, Goldscore = 0, GameCount = 0, GameTime = DateTime.Now };
                        payDay.Users.Add(user);
                        payDay.SaveChanges();
                    }
                    MessageBox.Show("You have successfully registered. Hello to Payday Panic");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ErrorCodes.DBSCon2202.ToString());
                }

            }
            IsLoading= false;
        }
        #endregion
       

           #region ------------------------- Commands ------------------------------------------------------------------------
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

        private void SignInCommandExecute(object parameter)
        {
            Thread thread = new Thread(DataBaseConect);
            thread.Start();
            
        }
        #endregion
    }
}
