using Common.Command;
using PayDay.Events;
using Microsoft.Practices.Prism.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Data;
using System.Windows.Controls;
using DataBase.Context;
using Services.Services;
using System.IO;

namespace PayDay.ViewModels
{
    public class MainMenuModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// Instance of the game class.
        /// </summary>
        public Game Game;
        /// <summary>
        /// View that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        private UserControl viewLeft;

        /// <summary>
        /// Instance of the rulesView view.
        /// </summary>
        private RulesView rulesView;

        /// <summary>
        /// Instance of the highscoreView view.
        /// </summary>
        private HighscoreVew highscoreVew;

        /// <summary>
        /// Instance of the statisticsView view.
        /// </summary>
        private StatisticsView statisticsView;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="MainMenuModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="username"></param>
        public MainMenuModel(IEventAggregator eventAggregator, string username) : base(eventAggregator)
        {
            this.Game = new Game(1000, username, 0, 1, 1);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            this.ExitCommand = new ActionCommand(this.ExitCommandExecute, this.ExitCommandCanExecute);
            this.PlayCommand = new ActionCommand(this.PlayCommandExecute, this.PlayCommandCanExecute);
            this.RulleCommand = new ActionCommand(this.RullesCommandExecute, this.RullesCommandCanExecute);
            this.TopPlayerCommand = new ActionCommand(this.TOPCommandExecute, this.RullesCommandCanExecute);
            this.LogOutCommand = new ActionCommand(this.LogOutCommandExecute, this.PlayCommandCanExecute);
            this.StatisticsCommand = new ActionCommand(this.StatisticsCommandExecute, this.RullesCommandCanExecute);

            rulesView = new RulesView();
            RulesViewModel rulesViewModel = new RulesViewModel(this.EventAggregator);
            this.rulesView.DataContext = rulesViewModel;
            this.highscoreVew = new HighscoreVew();
            HighscoreViewModel highscoreViewModel = new HighscoreViewModel(this.EventAggregator);
            highscoreVew.DataContext = highscoreViewModel;
            this.ViewLeft = this.rulesView;
            statisticsView = new StatisticsView();
            StatisticViewModel statisticViewModel = new StatisticViewModel(this.EventAggregator, this.Game);
            this.statisticsView.DataContext = statisticViewModel;
        }

        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets the exit button command.
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the play button command.
        /// </summary>
        public ICommand PlayCommand { get; private set; }

        /// <summary>
        /// Gets the rulle button command.
        /// </summary>
        public ICommand RulleCommand { get; private set; }

        /// <summary>
        /// Gets the logout command.
        /// </summary>
        public ICommand LogOutCommand { get; private set; }

        /// <summary>
        /// Gets the TopPlayer button command.
        /// </summary>
        public ICommand TopPlayerCommand { get; private set; }

        /// <summary>
        /// Gets the statistics command.
        /// </summary>
        public ICommand StatisticsCommand { get; private set; }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        public UserControl ViewLeft
        {
            get { return viewLeft; }
            set 
            {
                if(viewLeft != value)
                {
                    viewLeft = value;
                    this.OnPropertyChanged(nameof(ViewLeft));
                }
                
            }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the exit command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ExitCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the exit button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void ExitCommandExecute(object parameter)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Determines wheter the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool PlayCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the play button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void PlayCommandExecute(object parameter)
        {
            using(var context = new PayDayContext())
            {
                var item = context.Statistics.Where(s => s.User.UserName == Game.Username).ToList();
                item[0].GameCount++;
                context.SaveChanges();
            }
            GameView gameView = new GameView();
            GameViewModel gameViewModel = new GameViewModel(this.EventAggregator, this.Game);
            gameView.DataContext = gameViewModel;
            this.EventAggregator.GetEvent<GameViewDataChageEvent>().Publish(gameView);
        }

        /// <summary>
        /// Determines wheter the play command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool RullesCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the rulle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RullesCommandExecute(object parameter)
        {
            this.ViewLeft = this.rulesView;
        }

        /// <summary>
        /// Ocures when the user click the statistics button.
        /// </summary>
        /// <param name="parameter"></param>
        private void StatisticsCommandExecute(object parameter)
        {
            this.ViewLeft = this.statisticsView;
        }
        /// <summary>
        /// Ocures when the user clicks the rulle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void TOPCommandExecute(object parameter)
        {
            this.ViewLeft = this.highscoreVew;
        }

        /// <summary>
        /// Ocures when the user clicks the logout button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void LogOutCommandExecute(object parameter)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PayDay";
            string file = path + "\\login.txt";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            LogInView logInView = new LogInView();
            LogInViewModel logInViewModel = new LogInViewModel(this.EventAggregator);
            logInView.DataContext = logInViewModel;
            this.EventAggregator.GetEvent<LogInDataChangeEvent>().Publish(logInView);
        }
        #endregion
    }
}
