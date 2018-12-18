using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Models.User
{
    class User
    {
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
        public static string ApiKey { get; set; }

        public static string GetUserName()
        {
            try
            {
                string userName = File.ReadLines(Paths.AppData + @"\WVA Data Import\User\UserName.txt").Skip(0).Take(1).First();      
                return userName;
            }
            catch
            {
                return "";
            }
        }
    }
}
