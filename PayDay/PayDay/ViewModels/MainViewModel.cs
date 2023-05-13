using PayDay.Events;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Services.Services;
using System.IO;

namespace PayDay.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// View that is currently bound to the <see cref="ContentControl"/>.
        /// </summary>
        private UserControl currentView;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            MainViewCommandExecute();
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Subscribe(this.OnGameViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<RegisterDataChageEvent>().Subscribe(this.OnSignInViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Subscribe(this.OnMainMenuViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Subscribe(this.OnGameOverViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<LogInDataChangeEvent>().Subscribe(this.OnLogInViewChanged, ThreadOption.UIThread);
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        public UserControl CurrentView
        {
            get { return currentView; }

            set 
            { 
                if (currentView != value)
                {
                    this.currentView = value;
                    this.OnPropertyChanged(nameof(currentView));
                }
            }
        }

        #endregion

        #region  ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// Init view and viewmodel
        /// </summary>
        private void MainViewCommandExecute()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PayDay";
            string file = path + "\\login.txt";
            if (File.Exists(file))
            {
                AutoLogIn.ReadLogIn(out List<string> userinfo);
                if (RegisterServices.LogIn(userinfo[0], userinfo[1]))
                {
                    MainMenu mainMenu = new MainMenu();
                    MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator, userinfo[0]);
                    mainMenu.DataContext = mainMenuModel;
                    this.CurrentView = mainMenu;

                }
            }
            else
            {
                LogInView logInView = new LogInView();
                LogInViewModel logInViewModel = new LogInViewModel(this.EventAggregator);
                logInView.DataContext = logInViewModel;
                this.CurrentView = logInView;
            }
            
        }
       
        private void OnGameViewChanged(GameView gameview)
        {
            this.CurrentView = gameview;
        }
        private void OnLogInViewChanged(LogInView logInView)
        {
            this.CurrentView = logInView;
        }
        private void OnSignInViewChanged(SignInView signInView)
        {
            this.CurrentView = signInView;
        }

        private void OnMainMenuViewChanged(MainMenu mainMenu)
        {
            this.CurrentView = mainMenu;
        }
        private void OnGameOverViewChanged(GameEndView gameEndView)
        {
            this.CurrentView = gameEndView;
        }
        #endregion

    }
}
