using DataBase.Context;
using DataBase.Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using PayDay.Events;
using System.Threading;
using Common.Command;
using System.Windows.Media;
using Data;
using Services.Services;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Media.TextFormatting;
using System.Collections.Specialized;
using PayDay.Views;
using Services;

namespace PayDay.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// List of items from the shop.
        /// </summary>
        private ObservableCollection<ShopItems> shopItems;
        /// <summary>
        /// The amount of gold the player has.
        /// </summary>
        private int goldCount;
        /// <summary>
        /// Game data.
        /// </summary>
        private Game game;
        /// <summary>
        /// The color for the text.
        /// </summary>
        private Brush color;

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initialize a new instance of the <see cref="ShopViewModel"/> class.
        /// </summary>
        /// <param name="roundRobinCollection">List of points for the gold price graph.</param>
        /// <param name="game">Game data</param>
        /// <param name="shopItems">List of items from the shop.</param>
        public ShopViewModel(IEventAggregator eventAggregator, RoundRobinCollection roundRobinCollection, Game game, ObservableCollection<ShopItems> shopItems) : base(eventAggregator) 
        {
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChange, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<ShopItemsDataChangeEvent>().Subscribe(this.OnShopItemChange, ThreadOption.UIThread);
            this.ShopItems = new ObservableCollection<ShopItems>();
           this.ShopItems = shopItems;
            this.BuyCommand = new ActionCommand(this.BuyCommandExecuted, this.BuyCommandCanExecute);
            this.SellCommand = new ActionCommand(this.SellCommandExecuted, this.SellCommandCanExecute);
            this.BuyGameCommand = new ActionCommand(this.BuyGameCommandExecuted, this.BuyGameCommandCanExecute);
            this.SellGameCommand = new ActionCommand(this.SellGameCommandExecuted, this.SellGameCommandCanExecute);
            this.SellMaxCommand = new ActionCommand(this.SellMaxCommandExecuted, this.SellCommandCanExecute);
            this.BuyMaxCommand = new ActionCommand(this.BuyMaxCommandExecuted, this.BuyCommandCanExecute);
            this.Game = game;
            ProcessorTime = new RoundRobinCollection(100);
            ProcessorTime = roundRobinCollection;
            this.Color = Brushes.Red;

        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets the list of points for the gold price graph.
        /// </summary>
        public RoundRobinCollection ProcessorTime { get; }

        /// <summary>
        /// Gets or sets the List of items from the shop.
        /// </summary>
        public ObservableCollection<ShopItems> ShopItems
        {
            get { return this.shopItems; }
            set
            {
                this.shopItems= value;
            }
        }

        /// <summary>
        /// Gets the buy game button.
        /// </summary>
        public ICommand BuyGameCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the sell game button.
        /// </summary>
        public ICommand SellGameCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the buy gold button.
        /// </summary>
        public ICommand BuyCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the sell gold button.
        /// </summary>
        public ICommand SellCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the sell max gold button.
        /// </summary>
        public ICommand SellMaxCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the buy max gold button.,
        /// </summary>
        public ICommand BuyMaxCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the game data.
        /// </summary>
        public Game Game
        {
            get { return this.game; }
            set 
            {  
                this.game = value;
                this.OnPropertyChanged(nameof(this.Game));
                if (this.Game.GoldPrice < 50)
                {
                    this.Color = Brushes.Green;
                }
                else
                {
                    this.Color = Brushes.Red;   
                }

            }
        }

        /// <summary>
        /// Gets or sets the gold that the player has.
        /// </summary>
        public string GoldCount
        {
            get { return this.goldCount.ToString(); }
            set 
            {
                int.TryParse(value, out this.goldCount);
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        public Brush Color
        {
            get
            {
                return this.color;
            }
            set 
            {
                this.color = value;
                this.OnPropertyChanged(nameof(this.Color));
            }
        }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void OnGameDataChange(Game game)
        {
            if(this.Game.GoldPrice != game.GoldPrice)
            {
                this.ProcessorTime.Push((float)this.Game.GoldPrice);
            }
            this.Game = game;
        }
        private void OnShopItemChange(ObservableCollection<ShopItems> shopItems)
        {
            this.ShopItems = shopItems;
        }

        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Determines wheter buy gold can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the commsnd.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool BuyCommandCanExecute(object parameter)
        {
            return true;

        }

        /// <summary>
        /// Determines wheter sell gold can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool SellCommandCanExecute(object parameter)
        {
            if(this.Game.Gold > 0)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// Ocures when the user clicks the buy button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void BuyCommandExecuted(object parameter)
        {
            double money = this.Game.GoldPrice * this.goldCount;
            this.Game.BuyGold(this.goldCount);
            DataBaseService.BuyGoldPlusDatabase(this.Game,money, out ErrorCodes errorCodes);
            ErrorServices.ShowError(errorCodes);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
        }

        /// <summary>
        /// Determines wheter buy game can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the commsnd.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool BuyGameCommandCanExecute(object parameter)
        {
            foreach (var item in this.ShopItems)
            {
                if(item.ItemID == Convert.ToInt16(parameter))
                {
                    if(item.InStock > 0)
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        /// <summary>
        /// Ocures when the user clicks the buy game button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void BuyGameCommandExecuted(object parameter)
        {
            foreach (var item in this.ShopItems)
            {
                if(item.ItemID == Convert.ToInt32(parameter))
                {
                    this.Game.EditMoney(-item.Price);
                    this.Game.Items.Add(item);
                    item.InStock--;
                    DataBaseService.BuyGamePlusDatabase(this.Game, item.Price, out ErrorCodes errorCodes);
                    ErrorServices.ShowError(errorCodes);
                    this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                }
            }
        }

        /// <summary>
        /// Ocures when the user clicks the sell gold button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellCommandExecuted(object parameter)
        {
            ErrorCodes errorCodes = new ErrorCodes();
            double money = this.goldCount * this.Game.GoldPrice;
            if (this.Game.Gold >= this.goldCount)
            {
                this.Game.Gold = this.Game.Gold - this.goldCount;
                this.Game.EditMoney(money);
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                DataBaseService.SellGoldPlusDatabase(this.Game, money, out errorCodes);
            }
            else
            {
                MessageBox.Show("Du kannst nicht so viel Gold verkaufen!");
            }
            ErrorServices.ShowError(errorCodes);
        }

        /// <summary>
        ///  Determines wheter sell game can be executed.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool SellGameCommandCanExecute(object parameter)
        {
            foreach(var item in this.Game.Items)
            {
                if(item.ItemID == Convert.ToInt32(parameter))
                {
                    return true;
                }
            }
            return false;

        }
        /// <summary>
        /// Ocures when the user clicks the sell button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellGameCommandExecuted(object parameter)
        {
            ErrorCodes errorCodes = new ErrorCodes();
            for (int i = 0; i < this.Game.Items.Count; i++)
            {
                if (this.Game.Items[i].ItemID == Convert.ToInt32(parameter))
                {
                    this.Game.EditMoney(this.Game.Items[i].Price);
                    foreach (var item in this.ShopItems)
                    {
                        if(item.ItemID == this.Game.Items[i].ItemID)
                        {
                            item.InStock++;
                        }
                        DataBaseService.SellGamePlusDatabase(this.Game, item.Price, out errorCodes);
                    }
                    this.Game.Items.RemoveAt(i);
                    this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                }
            }
            ErrorServices.ShowError(errorCodes);
        }
        /// <summary>
        /// Ocures when the user clicks the sell max button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellMaxCommandExecuted(object parameter)
        {
            double money = this.Game.Gold * this.Game.GoldPrice;
            this.Game.EditMoney(money);
            DataBaseService.SellGoldPlusDatabase(this.Game, money, out ErrorCodes errorCodes);
            this.Game.Gold = this.Game.Gold - this.Game.Gold;
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            ErrorServices.ShowError(errorCodes);
        }

        /// <summary>
        /// Ocures when the user clicks the buy max button.
        /// </summary>
        /// <param name="parameter">Data use by the command</param>
        private void BuyMaxCommandExecuted(object parameter)
        {
            
            int buygold = (int)Math.Floor(this.Game.Money / this.Game.GoldPrice);
            if (buygold > 0)
            {
                double losemoney = buygold * this.Game.GoldPrice;
                
                DataBaseService.BuyGoldPlusDatabase(this.Game, losemoney, out ErrorCodes errorCodes);
                ErrorServices.ShowError(errorCodes);
                this.Game.Gold += buygold;
                this.Game.EditMoney(-(losemoney));
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            }
        }
        #endregion
    }
}
