using Common.Command;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.Services;
using Data;
using System.Threading;
using System.ComponentModel;
using DataBase.Context;

namespace PayDay.ViewModels
{
    public class GameEndViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private int newelo;
        private int elo;
        string username;
        string stage;
        private Game game;
        private BackgroundWorker backgroundWorker;
        private string rankPicture;
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public GameEndViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator)
        {
            this.ButtonNext = new ActionCommand(this.NextCommandExecuted, this.NextCommandCanExecute);
            newelo = GameEndServices.CountElo(game,out elo);
            username = game.Username;
            this.game = game;
            stage = "elo";
            backgroundWorker = new BackgroundWorker();
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand ButtonNext { get; private set; }

        public int Elo
        {
            get { return this.elo; }
            set 
            { 
                this.elo = value;
                this.OnPropertyChanged(nameof(this.Elo));
            }
        }

        public string RankPicture
        {
            get { return this.rankPicture; }
            set 
            {
                this.rankPicture = value; 
                this.OnPropertyChanged(nameof(this.RankPicture));
            }
        }
        #endregion



        #region ------------------------- Private helper ------------------------------------------------------------------
        private void eloAdjustment(object sender, DoWorkEventArgs e)
        {
            if (this.newelo > this.Elo)
            {
                for (int i = 0; i < this.newelo-this.Elo; i++)
                {
                    this.Elo++;
                    Thread.Sleep(100);
                }
            }
            else
            {
                for (int i = 0; i < this.Elo-this.newelo; i++)
                {
                    this.Elo--;
                    Thread.Sleep(100);
                }
            }
            this.RankPicture = GameEndServices.RankUpgrade(this.Elo);
        }

        public void SaveData(object sender, RunWorkerCompletedEventArgs e)
        {
            using (var context = new PayDayContext())
            {
                var items = context.Highscore.Where(f => f.User.UserName == game.Username).ToList();

                items[0].Elo = this.Elo;
                context.SaveChanges();
            }
        }
        #endregion



        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the register command be executed.
        /// </summary>
        /// <param name="parameter">Sata used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool NextCommandCanExecute(object parameter)
        {
            if (backgroundWorker.IsBusy)
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }

        /// <summary>
        /// Ocures when the user clicks the register button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void NextCommandExecuted(object parameter)
        {
            switch (this.stage)
            {
                case "elo": 
                    {
                        
                        backgroundWorker.DoWork += this.eloAdjustment;
                        backgroundWorker.RunWorkerCompleted += SaveData;
                        backgroundWorker.RunWorkerAsync();
                        GameEndServices.SaveDateaStatic(game);
                        this.stage = "window";
                        break;
                    }
                case "window":
                    {
                        MainMenu mainMenu = new MainMenu();
                        MainMenuModel mainMenuModel = new MainMenuModel(this.EventAggregator, username);
                        mainMenu.DataContext = mainMenuModel;
                        this.EventAggregator.GetEvent<MainMenuDataChangeEvent>().Publish(mainMenu);
                        break;
                    }
            }
           
        }
        #endregion


    }
}
