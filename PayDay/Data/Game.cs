using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;

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
        public List<Items> Items;

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
            Items= new List<Items>();
            this.Money=money;
            this.Username=username;
            this.Highscore=highscore;
            this.wins = win;
            this.casinoCount =casinoCount;
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
            get { return this.money; }
            set { this.money = value; }
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
        #endregion
    }
}
