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
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class GameEndViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary> The new elo of player. </summary>
        private int newelo;
        /// <summary> The elo of the player. </summary>
        private int elo;
        /// <summary> Username of player. </summary>
        string username;
        /// <summary> Stage system. </summary>
        string stage;
        /// <summary> Game round data. </summary>
        private Game game;
        /// <summary> Rank upgrade dispacerTimer. </summary>
        private DispatcherTimer dispatcherTimer;
        /// <summary> The picture of the player rank. </summary>
        private string rankPicture;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="GameEndViewModel"/> class.
        /// </summary>
        /// <param name="game">game datas</param>
        public GameEndViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator)
        {
            this.ButtonNext = new ActionCommand(this.NextCommandExecuted, this.NextCommandCanExecute);
            newelo = GameEndServices.CountElo(game,out elo);
            username = game.Username;
            this.game = game;
            stage = "elo";
            this.dispatcherTimer = new DispatcherTimer();
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary> Gets the mainmenu view button command. </summary>
        public ICommand ButtonNext { get; private set; }

        /// <summary> Gets or sets the the player elo. </summary>
        public int Elo
        {
            get { return this.elo; }
            set 
            { 
                this.elo = value;
                this.OnPropertyChanged(nameof(this.Elo));
            }
        }

        /// <summary> Gets or sets the player rank picture. </summary>
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
        /// <summary> Caunt the new player elo. </summary>
        private void EloAdjustment(object sender, EventArgs e)
        {
            if (this.newelo > this.Elo)
            {
                this.Elo++;
            }
            else
            {
                this.Elo--;
            }
            
            this.RankPicture = GameEndServices.RankUpgrade(this.Elo);
            if(this.Elo == this.newelo)
            {
                dispatcherTimer.Stop();
                this.stage = "window";
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
            return true;
           
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
                        this.stage = "finish";
                        this.dispatcherTimer.Interval = TimeSpan.FromTicks(20);
                        this.dispatcherTimer.Tick += this.EloAdjustment;
                        this.dispatcherTimer.Start();
                        DataBaseService.SaveData(this.newelo, this.game);
                        GameEndServices.SaveDateaStatic(game);
                        
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
                case "finish":
                    {
                        dispatcherTimer.Stop();
                        this.stage = "window";
                        this.Elo = this.newelo;
                        break;
                    }
            }
           
        }
        #endregion
    }
}
