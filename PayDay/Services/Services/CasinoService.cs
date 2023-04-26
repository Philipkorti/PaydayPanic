using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CasinoService
    {
        private List<string> casinoList = new List<string>{"\\Pictures\\seven.png", "\\Pictures\\paydayicon.png", "\\Pictures\\CasinoHerz.png"};
        
        public void CalculateWin(double winrate, out List<string> list2)
        {
            List<string> list = new List<string>() { "\\Pictures\\paydayicon.png", "\\Pictures\\paydayicon.png","\\Pictures\\paydayicon.png" };
            double win = 1 - winrate;
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

           
        }

        public int Win(List<string> list2, int bet, ref Game game)
        {
            int money = 0;
            if (list2[0] == list2[1] && list2[0] == list2[2])
            {
                game.Wins++;
                game.CasinoCount++;
                switch (list2[0])
                {
                    case "\\Pictures\\paydayicon.png":
                        {
                            money += bet * 100;
                            break;
                        }
                    case "\\Pictures\\seven.png":
                        {
                            money += bet * 10;
                            break;
                        }
                    case "\\Pictures\\CasinoHerz.png":
                        {
                            money += bet *5;
                            break;
                        }
                }
            }
            else
            {
                game.CasinoCount++;
            }
            return money;
        }
    }
}
