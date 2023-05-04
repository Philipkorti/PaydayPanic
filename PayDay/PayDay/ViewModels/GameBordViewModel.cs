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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PayDay.ViewModels
{
    public class GameBordViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private PointCollection points;
        private string windowsstate;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="GameBordViewModel"/> class.
        /// </summary>
        /// <param name="game">Instance of the class game.</param>
        public GameBordViewModel(IEventAggregator eventAggregator, Game game) : base(eventAggregator)
        {
            // Commands
            CasinoCommand = new ActionCommand(this.CasinoCommandExecuted, this.CasinoCommandCanExecute);
            ShopCommand = new ActionCommand(this.ShopCommandExecuted, this.ShopCommandCanExecute);
            this.EventAggregator.GetEvent<WindowstateDataChangeEvent>().Subscribe(this.OnWindowstateDataChange, ThreadOption.UIThread);
            Game = game;
            Thread thread = new Thread(this.GoldPrice);
            //thread.Start();
            points = new PointCollection();
            thread.Start();

        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets the casino view button command.
        /// </summary>
        public ICommand CasinoCommand { get; private set; }

        /// <summary>
        /// Gets the shop view button command.
        /// </summary>
        public ICommand ShopCommand { get; private set; }

        /// <summary>
        /// Gets or sets the game data.
        /// </summary>
        public Game Game { get; set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void GoldPrice()
        {
            // Count gold price for shop.
            do
            {
                this.Game = CountGoldService.GoldpriceCount(this.Game);

                // Whait for 2 minutes.
                Thread.Sleep(5000);

                // Check of gameover.
            } while (!this.Game.GameEnd);

        }
        private void OnWindowstateDataChange(string windowstate)
        {
            this.windowsstate = windowstate;
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter the students view command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        public bool CasinoCommandCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Ocures when the user clicks the casino view button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        public void CasinoCommandExecuted(object parameter)
        {
            CasinoView casinoView = new CasinoView();
            CasinoViewModel casinoViewModel = new CasinoViewModel(this.EventAggregator, this.Game);
            casinoView.DataContext = casinoViewModel;
            this.EventAggregator.GetEvent<CsinoViewDataChangeEvent>().Publish(casinoView);
        }

        /// <summary>
        /// Determines wheter the students view command can be executed.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        /// <returns><c>true</c> if the command can be exevuted otherwise <c>false</c>.</returns>
        public bool ShopCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ocures when the user clicks the shop view button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        public void ShopCommandExecuted(object parameter)
        {
                points.Add(new System.Windows.Point(10 * points.Count, Math.Round(220 - this.Game.GoldPrice)));

            ShopView shopView = new ShopView();
            ShopViewModel shopViewModel = new ShopViewModel(this.EventAggregator, points, this.Game);
            shopView.DataContext = shopViewModel;
            this.EventAggregator.GetEvent<ShopViewDataChangeEvent>().Publish(shopView);
        }
        #endregion
    }
}
