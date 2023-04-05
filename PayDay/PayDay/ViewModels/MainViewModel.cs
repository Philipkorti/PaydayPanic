﻿using Data.Events;
using Microsoft.Practices.Prism.Events;
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
            this.EventAggregator.GetEvent<ManMenuDataChageEvent>().Subscribe(this.OnViewChanged, ThreadOption.UIThread);
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
            MainMenu mainMenuView = new MainMenu();
            MainMenuModel mainMenuViewModel = new MainMenuModel(this.EventAggregator);
            mainMenuView.DataContext = mainMenuViewModel;
            this.CurrentView = mainMenuView;
        }
       
        private void OnViewChanged(GameView gameview)
        {
            this.CurrentView = gameview;
        }

    }
}
