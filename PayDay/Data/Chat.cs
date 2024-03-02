using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Chat
    {
        private string textPlayerOne;
        private string textPlayerTwo;


        public string TextPLayerOne
        {
            get { return textPlayerOne; }
            set 
            {
                textPlayerOne = value;
            }
        }
        public string TextPLayerTwo
        {
            get { return textPlayerTwo; }
            set { textPlayerTwo = value; }
        }
    }
}
