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

namespace PayDay.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private ObservableCollection<ShopItems> shopItems;
        private PointCollection points;
        private int goldCount;
        private Game game;
        private Brush color;

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ShopViewModel(IEventAggregator eventAggregator, RoundRobinCollection roundRobinCollection, Game game, ObservableCollection<ShopItems> shopItems) : base(eventAggregator) 
        {
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChange, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<ShopItemsDataChangeEvent>().Subscribe(this.OnShopItemChange, ThreadOption.UIThread);
            this.ShopItems = new ObservableCollection<ShopItems>();
           this.ShopItems = shopItems;
            this.BuyCommand = new ActionCommand(this.BuyCommandExecuted, this.BuyCommandCanExecute);
            this.SellCommand = new ActionCommand(this.SellCommandExecuted, this.BuyCommandCanExecute);
            this.BuyGameCommand = new ActionCommand(this.BuyGameCommandExecuted, this.BuyGameCommandCanExecute);
            this.SellGameCommand = new ActionCommand(this.SellGameCommandExecuted, this.SellGameCommandCanExecute);
            this.SellMaxCommand = new ActionCommand(this.SellMaxCommandExecuted, this.BuyCommandCanExecute);
            this.BuyMaxCommand = new ActionCommand(this.BuyMaxCommandExecuted, this.BuyCommandCanExecute);
            this.Game = game;
            this.Points = points;
            ProcessorTime = new RoundRobinCollection(100);
            ProcessorTime = roundRobinCollection;
            this.Color = Brushes.Red;

        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public RoundRobinCollection ProcessorTime { get; }
        public ObservableCollection<ShopItems> ShopItems
        {
            get { return this.shopItems; }
            set
            {
                this.shopItems= value;
            }
        }

        public PointCollection Points
        {
            get { return this.points; }
            set
            {
                this.points = value;
                this.OnPropertyChanged(nameof(this.Points));
            }
        }
        public ICommand BuyGameCommand
        {
            get; private set;
        }
        public ICommand SellGameCommand
        {
            get; private set;
        }
        public ICommand BuyCommand
        {
            get; private set;
        }

        public ICommand SellCommand
        {
            get; private set;
        }
        public ICommand SellMaxCommand
        {
            get; private set;
        }
        public ICommand BuyMaxCommand
        {
            get; private set;
        }

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
        public string GoldCount
        {
            get { return this.goldCount.ToString(); }
            set 
            {
                int.TryParse(value, out this.goldCount);
            }
        }

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
        /// Determines wheter the rolle slot machine can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the commsnd.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool BuyCommandCanExecute(object parameter)
        {
            return true;

        }
        /// <summary>
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void BuyCommandExecuted(object parameter)
        {
            this.Game.Gold = this.Game.Gold + this.goldCount;
            this.Game.EditMoney(-(this.goldCount * this.Game.GoldPrice));
            if (this.Game.Money < 0)
            {
                GameEndView gameEndView = new GameEndView();
                GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator, this.Game);
                gameEndView.DataContext = gameEndViewModel;
                this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
            }
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
        }

        /// <summary>
        /// Determines wheter the rolle slot machine can be executed.
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
        /// Ocures when the user clicks the rolle button.
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
                    
                    this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                }
            }
            if (this.Game.Money < 0)
            {
                GameEndView gameEndView = new GameEndView();
                GameEndViewModel gameEndViewModel = new GameEndViewModel(this.EventAggregator, this.Game);
                gameEndView.DataContext = gameEndViewModel;
                this.EventAggregator.GetEvent<GameEndViewDataChangeEvent>().Publish(gameEndView);
            }
        }

        /// <summary>
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellCommandExecuted(object parameter)
        {
            if (this.Game.Gold >= this.goldCount)
            {
                this.Game.Gold = this.Game.Gold - this.goldCount;
                this.Game.EditMoney(this.goldCount * this.Game.GoldPrice);
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            }
            else
            {
                MessageBox.Show("Du kannst nicht so viel Gold verkaufen!");
            }
           
        }

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
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellGameCommandExecuted(object parameter)
        {
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
                    }
                    this.Game.Items.RemoveAt(i);
                    this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                }
            }
        }
        private void SellMaxCommandExecuted(object parameter)
        {
            this.Game.EditMoney(this.Game.Gold * this.Game.GoldPrice);
            this.Game.Gold = this.Game.Gold - this.Game.Gold;
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
        }

        private void BuyMaxCommandExecuted(object parameter)
        {
            do
            {
                this.Game.Gold = this.Game.Gold + 1;
                this.Game.EditMoney(-this.Game.GoldPrice);
                this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
            } while(this.Game.Money - this.Game.GoldPrice >= 0);
        }
        #endregion
    }
}
