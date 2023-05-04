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

namespace PayDay.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private List<Items> shopItems;
        private PointCollection points;
        private int windowheigh;
        private Game game;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        public ShopViewModel(IEventAggregator eventAggregator, PointCollection points, Game game) : base(eventAggregator) 
        {
            using(var context = new PayDayContext())
            {
                this.ShopItems = context.Items.ToList();
            }
            this.BuyCommand = new ActionCommand(this.BuyCommandExecuted, this.BuyCommandCanExecute);
            this.Points = points;
            this.game = game;

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
        public ICommand BuyCommand
        {
            get; private set;
        }

        public int Windowheigh
        {
            get { return this.windowheigh;}
            set 
            { 
                this.windowheigh = value; 
                
                this.OnPropertyChanged(nameof(this.Windowheigh));
            }
        }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        private void OnPointColectionData(PointCollection points)
        {
            this.Points = points;
            
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
            int test = this.Windowheigh;
        }
        #endregion
    }
}
