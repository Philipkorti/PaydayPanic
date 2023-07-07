using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Schema;

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
        /// <summary> The amount of money the player has won. </summary>
        private string winMoney;
        /// <summary> The color of money the player has won. </summary>
        private Brush color;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CasinoViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the class game.</param>
        public CasinoViewModel(IEventAggregator eventAggregator, Game game) :  base(eventAggregator) 
        {
            // Set the default pictures.
            this.ButtonImgSourceone = ConstData.CasinoSeven;
            this.ButtonImgSourcetwo = ConstData.CasinoHerz;
            this.ButtonImgSourcethre = ConstData.CasinoPaydayIcon;
            this.RoleCasino = new ActionCommand(this.RoleCommandExecuted, this.RoleCommandCanExecute);
            this.Game = new Game();
            this.Game = game;
            casinoRun = true;
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Publish(this.casinoRun);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChange, ThreadOption.UIThread);
            this.stake = 10;
            this.countRounds = 1;
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary> Gets or sets the buttonImgSourceOne of the slot machine. </summary>
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
        /// <summary> Gets or sets the mony the player won. </summary>
        public string WinMoney
        {
            get { return this.winMoney; }
            set
            {
                this.winMoney= value;
                this.OnPropertyChanged(nameof(this.WinMoney));
            }
        }

        /// <summary> Gets or sets if the casino is running. </summary>
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

        /// <summary> Gets or sets stake of the slot machine. </summary>
        public string Stake
        {
            get { return this.stake.ToString(); }
            set 
            {
                int.TryParse(value, out this.stake);
            }
        }

        /// <summary> Gets or sets CountRounds. </summary>
        public string CountRounds
        {
            get { return this.countRounds.ToString(); }
            set
            { 
                int.TryParse(value, out this.countRounds);
                this.OnPropertyChanged(nameof(this.CountRounds));
                
            }
        }


        /// <summary> Gets or sets the buttonImgSourceTwo of the slot machine. </summary>
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

        /// <summary> Gets or sets the buttonImgSourceThre of the slot machine. </summary>
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

        /// <summary> Gets the roleCasino button command. </summary>
        public ICommand RoleCasino { get; private set; }

        /// <summary> Get or sets the color of money the player has won. </summary>
        public Brush Color
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
        /// <summary>
        /// The method in charge of rolling the casino.
        /// </summary>
        private void Role()
        {
            List<string> roles = new List<string>() { "/Pictures/Rolle2.gif", "/Pictures/gifrolle.gif", "/Pictures/Rolle3.gif"};
            double moneyWin;
            Random random = new Random();
            CasinoService casinoService = new CasinoService();
            int count;
            for (int i = 0; i < this.countRounds; i++)
            {
                this.Game.EditMoney(-this.stake);
                DataBaseService.OutPutMoney(this.Game, this.stake, out ErrorCodes errorCodes);
                ErrorServices.ShowError(errorCodes);
                if (this.Game.GameEnd || this.Game.GameEndTime)
                {
                    break;
                }
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                this.WinMoney = "-" + Convert.ToString(this.Stake);
                this.Color = Brushes.Red;
                // Rolle the slot machine with a second thred. 
                count = random.Next(3000, 10000);
                this.ButtonImgSourceone = roles[random.Next(0,3)];
                this.ButtonImgSourcetwo = roles[random.Next(0, 3)];
                this.ButtonImgSourcethre = roles[random.Next(0, 3)];
                Thread.Sleep(count);
                this.WinMoney = "";
                
                casinoService.CalculateWin(Game.WiningRateCasino, out List<string> winList);
                moneyWin = casinoService.Win(winList, this.stake, ref this.Game);
                ButtonImgSourceone = winList[0];
                Thread.Sleep(1000);
                ButtonImgSourcetwo = winList[1];
                Thread.Sleep(1000);
                ButtonImgSourcethre = winList[2];
                if(moneyWin > 0)
                {
                    this.Game.EditMoney(moneyWin);
                    this.WinMoney = "+" + Convert.ToString(moneyWin);
                    this.Color = Brushes.Green;
                    DataBaseService.InputPutMoney(this.Game, moneyWin, out errorCodes);
                    ErrorServices.ShowError(errorCodes);
                }
                
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                if (this.countRounds > 1)
                {
                    Thread.Sleep(5000);
                }
            }
            this.CasinoRun = true;
            this.EventAggregator.GetEvent<BackButtonCanExecuteDataChangeEvent>().Publish(this.casinoRun);
        }

        /// <summary>
        /// Sync game data.
        /// </summary>
        /// <param name="game">All game data.</param>
        private void OnGameDataChange(Game game)
        {
            this.Game = game;
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
            if(this.stake > 0)
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
        }
        #endregion
    }
}
