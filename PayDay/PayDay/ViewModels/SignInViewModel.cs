using Common.Command;
using Data.Data;
using Data.Models;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand SignIn { get; private set; }
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public SignInViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            SignIn = new ActionCommand(this.SignInCommandExecute, this.SignInCommandCanExecute);
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public string Username
        {
            get { return username; }
            set
            {
                UsernameCount = value.Length;
                if (this.username != value && UsernameCount <= 50)
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
                password = ComputeSha256Hash(value);
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
        #endregion



            #region ------------------------- Private helper ------------------------------------------------------------------
            static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
            using(var payDay = new PayDayContext())
            {
                var user = new User() {UserName = this.Username, Password = password, Elo = Convert.ToString(Ranks.Bronze), highscore = 0, Goldscore = 0};
                payDay.Users.Add(user);
                payDay.SaveChanges();
            }
            MessageBox.Show("Xou have successfully registered. Hello to Payday Panic");
        }
        #endregion
    }
}
