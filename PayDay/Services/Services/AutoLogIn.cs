using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AutoLogIn
    {
        public static void CreateFile(string username, string password)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PayDay";
            string file = path + "\\login.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(file))
            {
                using (FileStream fileStream = File.Create(file))
                {
                    byte[] usernamebyte = new UTF8Encoding(true).GetBytes(username);
                    byte[] passwordbyte = new UTF8Encoding(true).GetBytes(password);
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);

                    fileStream.Write(usernamebyte, 0, usernamebyte.Length);
                    fileStream.Write(newline, 0, newline.Length);
                    fileStream.Write(passwordbyte, 0, passwordbyte.Length);
                }
            }
        }

        public static void ReadLogIn(out List<string> userinfo)
        {
            userinfo = new List<string>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PayDay";
            string file = path + "\\login.txt";
            using (StreamReader sr = new StreamReader(file))
            {
               while(!sr.EndOfStream)
               {
                    userinfo.Add(sr.ReadLine());
               }
            }
        }
    }
}
