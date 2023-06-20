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

    }
}
