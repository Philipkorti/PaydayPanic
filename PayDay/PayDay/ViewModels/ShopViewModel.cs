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

namespace PayDay.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private List<Items> shopItems;
        private PointCollection points;
        private int goldCount;
        private Game game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ShopViewModel(IEventAggregator eventAggregator, PointCollection points, Game game) : base(eventAggregator) 
        {
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Subscribe(this.OnGameDataChange, ThreadOption.UIThread);
            using(var context = new PayDayContext())
            {
                this.ShopItems = context.Items.ToList();
            }
            this.BuyCommand = new ActionCommand(this.BuyCommandExecuted, this.BuyCommandCanExecute);
            this.SellCommand = new ActionCommand(this.SellCommandExecuted, this.BuyCommandCanExecute);
            this.BuyCommand = new ActionCommand(this.BuyGameCommandExecuted, this.BuyGameCommandCanExecute);
            this.Game = game;
            this.Points = points;

        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public List<Items> ShopItems
        {
            get { return this.shopItems; }
            set { this.shopItems = value; }
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
        public ICommand BuyCommand
        {
            get; private set;
        }

        public ICommand SellCommand
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
            }
        }
        public int GoldCount
        {
            get { return this.goldCount; }
            set { this.goldCount = value; }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void OnGameDataChange(Game game)
        {
            this.Game = game;
            this.points.Add(new Point(10 * points.Count, Math.Round(220 - this.Game.GoldPrice)));
            this.Points = this.points;
            this.OnPropertyChanged(nameof(this.Points));
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
            this.Game.Gold = this.Game.Gold + this.GoldCount;
            this.Game.Money = this.Game.Money - (this.GoldCount * this.Game.GoldPrice);
            
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
        }

        /// <summary>
        /// Determines wheter the rolle slot machine can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the commsnd.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool BuyGameCommandCanExecute(object parameter)
        {
            return true;

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
                    this.Game.Money = this.Game.Money - item.Price;
                    this.Game.Items.Add(item);
                    this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
                }
            }
        }

        /// <summary>
        /// Ocures when the user clicks the rolle button.
        /// </summary>
        /// <param name="parameter">Data use by the command.</param>
        private void SellCommandExecuted(object parameter)
        {
            this.Game.Gold = this.Game.Gold - this.GoldCount;
            this.Game.Money = this.Game.Money + (this.GoldCount * this.Game.GoldPrice);
            this.EventAggregator.GetEvent<GameDataChangeEvent>().Publish(this.Game);
        }
        #endregion
    }
}
