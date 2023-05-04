using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class HighscoreViewData
    {
        private int userID;
        private string userName;
        private double highestScore;
        private string rank;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string UserName { get { return userName; } set { userName = value; } }
        public double HighestScore { get { return highestScore; } set { highestScore = value; } }
        public string Rank { get { return rank;} set { rank = value; } }
    }
}
