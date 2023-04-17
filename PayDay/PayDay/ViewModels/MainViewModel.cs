using PayDay.Events;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl currentView;

        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            MainViewCommandExecute();
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Subscribe(this.OnGameViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<RegisterDataChageEvent>().Subscribe(this.OnSignInViewChanged, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Subscribe(this.OnMainMenuViewChanged, ThreadOption.UIThread);
        }

        public ICommand MainMenuViewComman { get; private set; }
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

        private bool MainViewCommandCanExecute(object parameter)
        {
            return true;
        }

        private void MainViewCommandExecute()
        {
            //MainMenu mainMenuView = new MainMenu();
            //MainMenuModel mainMenuViewModel = new MainMenuModel(this.EventAggregator);
            //mainMenuView.DataContext = mainMenuViewModel;
            //this.CurrentView = mainMenuView;
            LogInView logInView = new LogInView();
            LogInViewModel logInViewModel = new LogInViewModel(this.EventAggregator);
            logInView.DataContext = logInViewModel;
            this.CurrentView = logInView;
        }
       
        private void OnGameViewChanged(GameView gameview)
        {
            this.CurrentView = gameview;
        }
        private void OnSignInViewChanged(SignInView signInView)
        {
            this.CurrentView = signInView;
        }

        private void OnMainMenuViewChanged(MainMenu mainMenu)
        {
            this.CurrentView = mainMenu;
        }

    }
}
