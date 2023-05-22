using Common.Command;
using Data;
using DataBase.Context;
using Microsoft.Practices.Prism.Events;
using PayDay.Events;
using PayDay.Views;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PayDay.ViewModels
{
    public class GameBordViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary> Gold price polilyne points. </summary>
        private RoundRobinCollection roundRobinCollection;
        /// <summary> Shop items list. </summary>
        private ObservableCollection<ShopItems> shopItems;
        /// <summary> Game time. </summary>
        DispatcherTimer dispatcherTimer;
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
            Game = game;
            roundRobinCollection = new RoundRobinCollection(100);
            roundRobinCollection.Push((float)this.Game.GoldPrice);
            this.shopItems = new ObservableCollection<ShopItems>();
            using (var context = new PayDayContext())
            {
                var item = context.Items.ToList();
                foreach (var shopitem in item)
                {
                    this.shopItems.Add(new ShopItems() { ItemID = shopitem.ItemID, PictureURL = shopitem.PictureURL, InStock = shopitem.InStock, Price = shopitem.Price, Title = shopitem.Title });
                }
            }
            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            dispatcherTimer.Tick += this.GoldPrice;
            dispatcherTimer.Start();
            

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
        /// <summary>
        /// Calculate gold price.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoldPrice(object sender, EventArgs e)
        {
            // Count gold price for shop.
            if (this.Game.GameEndTime || this.Game.Money < 0)
            {
                dispatcherTimer.Stop();
                this.Game.GameEndTime = false;
            }
            this.Game = CountGoldService.GoldpriceCount(this.Game);
            roundRobinCollection.Push((float)this.Game.GoldPrice);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            CountGoldService.GamePriceCalculate(shopItems);
            this.EventAggregator.GetEvent<ShopItemsDataChangeEvent>().Publish(this.shopItems);
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

            ShopView shopView = new ShopView();
            ShopViewModel shopViewModel = new ShopViewModel(this.EventAggregator, roundRobinCollection, this.Game, this.shopItems);
            shopView.DataContext = shopViewModel;
            this.EventAggregator.GetEvent<ShopViewDataChangeEvent>().Publish(shopView);
        }
        #endregion
    }
}
