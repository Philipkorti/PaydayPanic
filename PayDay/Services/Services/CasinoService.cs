using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CasinoService
    {
        private List<string> casinoList = new List<string>{"\\controller.png", "\\paydayicon.png", "\\CasinoHerz.png"};
        
        public bool CalculateWin(double winrate, out List<string> list2)
        {
            List<string> list = new List<string>() { "\\paydayicon.png", "\\paydayicon.png","\\paydayicon.png"};
            double win = 1 - winrate;
            bool playerwin = true;


            if(win > 5) 
            {
                for (int i = 3; i < 20; i++)
                {
                    if (i <= 10)
                    {
                        list.Add(casinoList[2]);
                    }
                    else
                    {
                        list.Add(casinoList[0]);
                    }
                }
            }
            else
            {
                for (int i = 3; i < 20; i++)
                {
                    if (i > 10)
                    {
                        list.Add(casinoList[2]);
                    }
                    else
                    {
                        list.Add(casinoList[0]);
                    }
                }
            }
            Random random= new Random();
            list2 = new List<string>();
            int count;
            for(int i = 0; i< 3; i++)
            {
                count =random.Next(0,19 - i);
                list2.Add(list[count]);
                list.RemoveAt(count);
            }

            for (int i = 0; i < list2.Count-1; i++)
            {
                if (list2[i] != list2[i+1])
                {
                    playerwin = false;
                    break;
                }
            }
            return playerwin;
        }

        public void Win()
        {

        }
        public void Lose()
        {

        }
    }
}
