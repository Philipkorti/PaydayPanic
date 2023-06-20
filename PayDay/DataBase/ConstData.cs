using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ConstData
    {
        public const int StringLengh = 50;
        public static string Path { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PayDay";
        public static string File = Path + @"\login.txt";
        public const string CasinoSeven = @"\Pictures\seven.png";
        public const string CasinoHerz = @"\Pictures\CasinoHerz.png";
        public const string CasinoPaydayIcon = "\\Pictures\\paydayicon.png";
        public const string DataBaseCon = "mongodb+srv://Test:Test@htl.3l5a5dj.mongodb.net?authSource=admin";
        public const string Bronze = @"\Pictures\bronze.png";
    }
}
