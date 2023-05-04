using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PayDay.ViewModels
{
    public class CasinoViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary> The source of the first picture in the oslot machine. </summary>
        private string buttonImgSourceone;
        /// <summary> The source of the second picture in the slot machine. </summary>
        private string buttonImgSourcetwo; 
        /// <summary> The source of the third picture in the slot machine. </summary>
        private string buttonImgSourcethre;
        /// <summary> The value of whether the slotmachine rolls. </summary>
        private bool casinoRun;
        /// <summary> The number of times the slot machine rotates. </summary>
        private int countRounds;
        /// <summary> The value for how much money is played. </summary>
        private int stake;
        private string winMoney;
        private string color;
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CasinoViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the class game.</param>
        public CasinoViewModel(IEventAggregator eventAggregator, Game game) :  base(eventAggregator) 
        {
            // Set the default pictures.
            this.ButtonImgSourceone = "\\Pictures\\seven.png";
            this.ButtonImgSourcetwo = "\\Pictures\\CasinoHerz.png";
            this.ButtonImgSourcethre = "\\Pictures\\paydayicon.png";
            this.RoleCasino = new ActionCommand(this.RoleCommandExecuted, this.RoleCommandCanExecute);
            
            this.Game = new Game();
            this.Game = game;
            casinoRun = true;
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Publish(this.casinoRun);
            this.Stake = 10;
            this.CountRounds = 1;
        }
        #endregion



        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets or sets the buttonImgSourceOne of the slot machine.
        /// </summary>
        public string ButtonImgSourceone
        {
            get { return this.buttonImgSourceone; }
            set
            {
                if (this.buttonImgSourceone != value)
                {
                    this.buttonImgSourceone = value;
                    this.OnPropertyChanged(nameof(this.ButtonImgSourceone));
                }
            }
        }
        public string WinMoney
        {
            get { return this.winMoney; }
            set
            {
                this.winMoney= value;
                this.OnPropertyChanged(nameof(this.WinMoney));
            }
        }

        public bool CasinoRun
        {
            get 
            {
                return this.casinoRun;
            }
            set 
            { 
                this.casinoRun = value;
                this.OnPropertyChanged(nameof(this.CasinoRun));

            }
        }

        /// <summary>
        /// Gets or sets stake of the slot machine.
        /// </summary>
        public int Stake
        {
            get { return this.stake; }
            set 
            {
                
                if (casinoRun)
                {
                    this.stake = value;
                }
               
            }
        }

        /// <summary>
        /// Gets or sets CountRounds.
        /// </summary>
        public int CountRounds
        {
            get { return this.countRounds; }
            set 
            {
                if(casinoRun)
                {
                    this.countRounds = value;
                    this.OnPropertyChanged(nameof(this.CountRounds));
                }
                
            }
        }


        /// <summary>
        /// Gets or sets the buttonImgSourceTwo of the slot machine. 
        /// </summary>
        public string ButtonImgSourcetwo
        {
            get { return buttonImgSourcetwo; }
            set
            {
                if (buttonImgSourcetwo != value)
                {
                    buttonImgSourcetwo = value;
                    this.OnPropertyChanged(nameof(this.ButtonImgSourcetwo));
                }
            }
        }
        public Game Game;

        /// <summary>
        /// Gets or sets the buttonImgSourceThre of the slot machine.
        /// </summary>
        public string ButtonImgSourcethre
        {
            get { return this.buttonImgSourcethre; }
            set
            {
                if (this.buttonImgSourcethre != value)
                {
                    this.buttonImgSourcethre = value;
                    this.OnPropertyChanged(nameof(this.ButtonImgSourcethre));
                }
            }
        }

        /// <summary>
        /// Gets the roleCasino button command.
        /// </summary>
        public ICommand RoleCasino { get; private set; }

        public string Color
        {
            get { return this.color; }
            set
            {
                this.color = value;
                this.OnPropertyChanged(nameof(this.Color));
            }
        }

       
        #endregion



        #region ------------------------- Private helper ------------------------------------------------------------------
        private void Role()
        {
            List<string> roles = new List<string>() { "/Pictures/Rolle2.gif", "/Pictures/gifrolle.gif", "/Pictures/Rolle3.gif"};
            double moneyWin;
            for (int i = 0; i < this.CountRounds; i++)
            {
                moneyWin = 0;
                this.Game.Money -= this.Stake;
                this.Game.Moneylose += this.Stake;
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                this.CountRounds--;
                this.WinMoney = "-" + Convert.ToString(this.Stake);
                this.Color = "Red";
                // Rolle the slot machine with a second thred. 
                Random random = new Random();
                int count = random.Next(3000, 10000);
                this.ButtonImgSourceone = roles[random.Next(0,3)];
                this.ButtonImgSourcetwo = roles[random.Next(0, 3)];
                this.ButtonImgSourcethre = roles[random.Next(0, 3)];
                Thread.Sleep(count);
                this.WinMoney = "";
                CasinoService casinoService = new CasinoService();
                CasinoService casinoService1 = new CasinoService();
                casinoService.CalculateWin(Game.WiningRateCasino, out List<string> list2);
                moneyWin = casinoService.Win(list2, this.Stake,ref this.Game);
                this.Game.Money += moneyWin;
                this.Game.MoneyWin += moneyWin;
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                ButtonImgSourceone = list2[0];
                ButtonImgSourcetwo = list2[1];
                ButtonImgSourcethre = list2[2];
                if(moneyWin > 0)
                {
                    this.WinMoney = "+" + Convert.ToString(moneyWin);
                    this.Color = "Green";
                }
               
                if(this.CountRounds > 1)
                {
                    Thread.Sleep(5000);
                }
                if (this.Game.GameEnd)
                {
                    break;
                }
            }
           
            this.CasinoRun = true;
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Publish(this.casinoRun);
        }

        /// <summary>
        /// Show the GameOver Screen
        /// </summary>
        private void ShowGameEndView()
        {
            GameEndView gameEndView = new GameEndView();
            GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator,Game);
            gameEndView.DataContext = gameEndViewModel;
            this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
        }
        #endregion



        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the rolle slot machine can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the commsnd.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool RoleCommandCanExecute(object parameter)
        {
            if(this.Stake > 0)
            {
                return casinoRun;
            }
            else
            {
                return false;
            }
           
        }
        /// <summary>
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RoleCommandExecuted(object parameter)
        {
            this.CasinoRun = false;
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Publish(this.casinoRun);
            Thread thread = new Thread(this.Role);
            thread.Start();
            Thread.Sleep(20);
            if (this.Game.GameEnd)
            {
                this.ShowGameEndView();
            }
            
        }

       
        #endregion
    }
}
