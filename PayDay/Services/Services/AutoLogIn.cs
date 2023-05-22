using Data;
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
        /// <summary>
        /// Create a new auto login file.
        /// </summary>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        public static void CreateFile(string username, string password)
        {
            if (!Directory.Exists(ConstData.Path))
            {
                Directory.CreateDirectory(ConstData.Path);
            }
            if (!File.Exists(ConstData.File))
            {
                using (FileStream fileStream = File.Create(ConstData.File))
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

        /// <summary>
        /// Auto login.
        /// </summary>
        /// <param name="userinfo">User informaion.</param>
        public static void ReadLogIn(out List<string> userinfo)
        {
            userinfo = new List<string>();
            using (StreamReader sr = new StreamReader(ConstData.File))
            {
               while(!sr.EndOfStream)
               {
                    userinfo.Add(sr.ReadLine());
               }
            }
        }
    }
}
