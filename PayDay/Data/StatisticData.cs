namespace Data
{
    public class StatisticData
    {
        /// <summary> Gets or sets the elo of the player. </summary>
        public int Elo { get; set; }

        /// <summary> Gets or sets the rank of the player. </summary>
        public string Rank { get; set; }

        /// <summary> Gets or sets the count of bought games. </summary>
        public int BoughtGamesCount { get; set; }

        /// <summary> Gets or sets the count of sold games. </summary>
        public int SoldGamesCount { get; set; }

        /// <summary> Gets or sets the amount of money the player have spent. </summary>
        public double OutputMoney { get; set; }

        /// <summary> Gets or sets the amount of money the player have taken. </summary>
        public double InPutMoney { get ; set; }

        /// <summary> Gets or sets the number of times the player has played. </summary>
        public int GameCount { get; set; }

        /// <summary> Gets or sets the amount of money the player wins. </summary>
        public double GameMoneyWin {get; set; }

        /// <summary> Gets or sets the amount of money the player lose. </summary>
        public double GameMoneyLoos { get; set; }

        /// <summary> Gets or sets the numbeer of wins in the casino. </summary>
        public int CasinoWins { get; set; }

        /// <summary> Gets or sets the number of times the player has played in the casino. </summary>
        public int CasinoCount { get; set; }

        /// <summary> Gets or sets the number of times the player has win with the seven in the casino. </summary>
        public int WinSeven { get; set; }

        /// <summary> Gets or sets the number of times the player has win with the moneybag in the casino. </summary>
        public int WinMoneybag { get; set; }

        /// <summary> Gets or sets the number of times the player has win with the heart in the  casino. </summary>
        public int WinHeart { get; set; }

        /// <summary>
        /// Initiaalizes a new instance of the <see cref="StatisticData"/> class.
        /// </summary>
        /// <param name="elo">Elo of the player.</param>
        /// <param name="rank">Rank of the player.</param>
        /// <param name="boughtGamesCount">Count of bought games of the player.</param>
        /// <param name="soldGamesCount">Count of sells games oft the player.</param>
        /// <param name="outputMoney">The amount of money the player have spent.></param>
        /// <param name="inputMoney">The amount of money the player have taken.></param>
        /// <param name="gameCount">The number of times the player has played.</param>
        /// <param name="gameMoneyWin">The amount of money the player wins.</param>
        /// <param name="gameMoneyloos">The amount of money the player lose.</param>
        /// <param name="casinoWins">The numbeer of wins in the casino.</param>
        /// <param name="casinoCount">The number of times the player has played in the casino.</param>
        /// <param name="winSeven">The number of times the player has win with the seven in the casino.</param>
        /// <param name="winMoneybag">The number of times the player has win with the moneybag in the casino.</param>
        /// <param name="winHeart">The number of times the player has win with the heart in the  casino.</param>
        public StatisticData(int elo, string rank, int boughtGamesCount, int soldGamesCount, double outputMoney, double inputMoney, 
            int gameCount, double gameMoneyWin, double gameMoneyloos, int casinoWins, int casinoCount, int winSeven, int winMoneybag, int winHeart)
        {
            this.Elo = elo;
            this.Rank = rank;
            this.BoughtGamesCount = boughtGamesCount;
            this.SoldGamesCount = soldGamesCount;
            this.OutputMoney = outputMoney;
            this.InPutMoney = inputMoney;
            this.GameCount = gameCount;
            this.GameMoneyWin = gameMoneyWin;
            this.GameMoneyLoos = gameMoneyloos;
            this.CasinoCount = casinoCount;
            this.WinSeven = winSeven;
            this.CasinoWins = casinoWins;
            this.WinMoneybag = winMoneybag;
            this.WinHeart = winHeart;
        }
    }
}
