namespace Data
{
    public class HighscoreViewData
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------

        /// <summary>
        /// This is the player ID
        /// </summary>
        private int userID;

        /// <summary>
        /// This is the player name.
        /// </summary>
        private string userName;
        /// <summary>
        /// Rank of the player
        /// </summary>
        private string rank;
        /// <summary>
        /// Elo of the player.
        /// </summary>
        private int elo;
        /// <summary>
        /// Picture URL of the rank picture.
        /// </summary>
        private string rankURL;
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary> Gets or sets the userid of the player. </summary>
        public int UserID
        {
            get => this.userID;
            set { userID = value; }
        }

        /// <summary> Gets or sets the username of the player. </summary>
        public string UserName { get { return userName; } set { userName = value; } }

        /// <summary> Gets or sets the rank of the player. </summary>
        public string Rank { get { return rank;} set { rank = value; } }

        /// <summary> Gets or sets the elo of the player. </summary>
        public int Elo { get { return this.elo; } set { elo = value; } }

        /// <summary> Gets or sets the rankURL of the rank. </summary>
        public string RankURL { get { return rankURL; } set { rankURL = value; } }
        #endregion
    }
}
