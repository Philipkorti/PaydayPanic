using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        #endregion



        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CasinoViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the class game.</param>
        public CasinoViewModel(IEventAggregator eventAggregator, Game game) :  base(eventAggregator) 
        {
            // Set the default pictures.
            this.ButtonImgSourceone = "\\paydayicon.png";
            this.ButtonImgSourcetwo = "\\CasinoHerz.png";
            this.ButtonImgSourcethre = "\\controller.png";

            this.RoleCasino = new ActionCommand(this.RoleCommandExecuted, this.RoleCommandCanExecute);
            this.Game = new Game();
            this.Game = game;
            casinoRun = true;
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
        #endregion



        #region ------------------------- Private helper ------------------------------------------------------------------
        private void Role()
        {
            // Rolle the slot machine with a second thred. 
            List<string> picture = new List<string>() { "\\CasinoHerz.png", "\\controller.png", "\\paydayicon.png"};
            Random random = new Random();
            int count = random.Next(300,3000);
            for (int i = 0; i < count; i++)
            {
                this.ButtonImgSourceone = picture[random.Next(0,3)];
                this.ButtonImgSourcetwo = picture[random.Next(0, 3)];
                this.ButtonImgSourcethre = picture[random.Next(0, 3)];
                Thread.Sleep(1);
            }
            casinoRun = true;
            CasinoService casinoService = new CasinoService();
            casinoService.CalculateWin(Game.WiningRateCasino, out List<string> list2);

            ButtonImgSourceone = list2[0];
            ButtonImgSourcetwo = list2[1];
            ButtonImgSourcethre = list2[2];
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
            return casinoRun;
        }
        /// <summary>
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void RoleCommandExecuted(object parameter)
        {
            casinoRun = false;
            this.Game.Money -= 10;
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            Thread thread = new Thread(this.Role);
            thread.Start();
        }
        #endregion
    }
}
