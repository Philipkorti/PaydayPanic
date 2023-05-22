using System;
using System.Collections.Generic;

namespace Data
{

    public class Game
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        /// <summary>
        /// Money of the player ingame.
        /// </summary>
        private double money;

        /// <summary>
        /// Username of the player.
        /// </summary>
        private string username;

        /// <summary>
        /// List of Items, what the player bought.
        /// </summary>
        List<ShopItems> items;
        /// <summary>
        /// Wins in the casino.
        /// </summary>
        private int wins;

        /// <summary>
        /// The count of how many times the casino has played.
        /// </summary>
        private int casinoCount;

        /// <summary>
        /// The highscore of the player.
        /// </summary>
        private int highscore;
        private double moneyLose;
        private double moneyWin;
        private int gold;
        private double goldprice;

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initiallizes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="money">Money of the player ingame.</param>
        /// <param name="username">Username of the player</param>
        /// <param name="highscore">The highscore of the player.</param>
        /// <param name="win">Wins in the casino.</param>
        /// <param name="casinoCount">The count of how many times the casino has playerd.</param>
        public Game(double money, string username, int highscore, int win, int casinoCount) 
        {
            // set values
            this.Money=money;
            this.Username=username;
            this.Highscore=highscore;
            this.wins = win;
            this.casinoCount =casinoCount;
            this.GoldPrice = 60;
            this.Items= new List<ShopItems>();
        }


        /// <summary>
        /// Initiallizes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game() { }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// Gets or sets the money of the game.
        /// </summary>
        public double Money
        {
            get { return Math.Round(this.money,2); }
         
            set { this.money = value; }
        }
        /// <summary>
        /// Gets or sets the gold price of the game.
        /// </summary>
        public double GoldPrice
        {
            get { return this.goldprice; }
            set { this.goldprice = value; }
        }

        /// <summary>
        /// Gets or sets the cold count of the player.
        /// </summary>
        public int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }
        /// <summary>
        /// Gets or sets the username of the game.
        /// </summary>
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        /// <summary>
        /// Gets the winingrate of the casino.
        /// </summary>
        public double WiningRateCasino
        {
            get 
            {
                return this.wins / this.casinoCount;
            }
        }

        /// <summary>
        /// Gets or sets the highscore of the game.
        /// </summary>
        public int Highscore
        {
            get { return this.highscore; }
            set { this.highscore = value; }
        }

        /// <summary>
        /// Sets the wins of the casino.
        /// </summary>
        public int Wins
        {
            set
            {
                this.wins = value;
            }
            get { return this.wins; }
        }

        /// <summary>
        /// Sets the count of how many times the casino has playerd.
        /// </summary>
        public int CasinoCount
        {
            set { this.casinoCount = value; }
            get { return this.casinoCount; }
        }

        /// <summary>
        /// Gets the game over
        /// </summary>
        public bool GameEnd
        {
            get 
            {
                if(this.Money < 0 && this.Gold> 0)
                {
                    do
                    {
                        this.Money += this.GoldPrice;
                        this.MoneyWin += this.GoldPrice;
                        this.Gold--;
                    } while (this.Money < 0 && this.Gold > 0);
                }
                return this.Money < 0 ? true : false; 
            }
        }

        /// <summary>
        /// Gets or sets the game time is over.
        /// </summary>
        public bool GameEndTime
        {
            set; get;
        }
        /// <summary>
        /// Gets or sets the mony what the player lose.
        /// </summary>
        public double MoneyLose
        {
            get{ return moneyLose; }
            set { moneyLose = value; }
        }

        /// <summary>
        /// Gets or sets the mony what the player win.
        /// </summary>
        public double MoneyWin
        {
            get { return moneyWin; }
            set { moneyWin = value; }
        }

        /// <summary>
        /// Gets or sets the item list of items waht the playedr bought.
        /// </summary>
        public List<ShopItems> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

        /// <summary>
        /// The medote edit the money of the player.
        /// </summary>
        /// <param name="money">This is player's money.</param>
        public void EditMoney(double money)
        {
            if(money< 0)
            {
                this.MoneyLose -= money;
            }
            else
            {
                this.MoneyWin += money;
            }
            this.Money += money;
        }

        /// <summary>
        /// The methode add gold to the Gold count of the player.
        /// </summary>
        /// <param name="goldCount">This is the count what the player bought.</param>
        public void BuyGold(int goldCount)
        {
            double editMoney = goldCount * this.GoldPrice;
            this.Gold += goldCount;
            this.Money -= editMoney;
            this.MoneyLose += editMoney;
        }
        #endregion
    }
}
