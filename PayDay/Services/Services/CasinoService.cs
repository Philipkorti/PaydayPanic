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
        /// <summary>
        /// Is calculated whether player have won
        /// </summary>
        /// <param name="winrate">Win probability of the player</param>
        /// <param name="winList">Win list of the casino icons</param>
        public void CalculateWin(double winrate, out List<string> winList)
        {
            double win = 1 - winrate;
            List<string> list = new List<string>();
            Random random = new Random();
            winList = new List<string>();
            int count;

            list.AddRange(WinListAdd(ConstData.CasinoPaydayIcon, 3));

            if(win > 5) 
            {
                list.AddRange(WinListAdd(ConstData.CasinoHerz, 9));
                list.AddRange(WinListAdd(ConstData.CasinoSeven, 8));
            }
            else
            {
                list.AddRange(WinListAdd(ConstData.CasinoHerz, 8));
                list.AddRange(WinListAdd(ConstData.CasinoSeven, 9));
            }
            
            for(int i = 0; i< 3; i++)
            {
                count =random.Next(0,19 - i);
                winList.Add(list[count]);
                list.RemoveAt(count);
            }
        }

        /// <summary>
        /// Calculates how much player have won
        /// </summary>
        /// <param name="winList">in list of the casino icons</param>
        /// <param name="bet">Player bet</param>
        /// <param name="game">Game data</param>
        /// <returns></returns>
        public int Win(List<string> winList, int bet, ref Game game)
        {
            int money = 0;
            ErrorCodes errorCodes = new ErrorCodes();
            if (winList[0] == winList[1] && winList[0] == winList[2])
            {
                game.Wins++;
                game.CasinoCount++;
                switch (winList[0])
                {
                    case ConstData.CasinoPaydayIcon:
                        {
                            money += bet * 100;
                            DataBaseService.PlusWinsCasino(game, 0, 0, 1, out errorCodes);
                            break;
                        }
                    case ConstData.CasinoSeven:
                        {
                            money += bet * 10;
                            DataBaseService.PlusWinsCasino(game, 1, 0, 0, out errorCodes);
                            break;
                        }
                    case ConstData.CasinoHerz:
                        {
                            money += bet *5;
                            DataBaseService.PlusWinsCasino(game, 0,1,0, out errorCodes);
                            break;
                        }
                }
            }
            else
            {
                game.CasinoCount++;
                DataBaseService.LoosPlusCasino(game, out errorCodes);
            }
            ErrorServices.ShowError(errorCodes);
            return money;
        }

        /// <summary>
        /// Win list add
        /// </summary>
        /// <param name="icon">casino icons</param>
        /// <returns></returns>
        private List<string> WinListAdd(string icon, int count)
        {
            List<string> casinoList = new List<string>();
            for (int i = 0; i < count; i++)
            {
                casinoList.Add(icon);
            }
            return casinoList;
        }
    }
}
