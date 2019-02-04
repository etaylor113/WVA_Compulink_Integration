using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Utility.File
{
    class Paths
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string Program_x86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string AppDir = Environment.CurrentDirectory;
        
        public static string ActNumDir = $@"{AppDir}\ActNum\";
        public static string ActNumFile = $@"{AppDir}\ActNum\ActNum.txt";

        public static string IpNumDir = $@"{AppDir}\ipConfig\";
        public static string IpNumFile = $@"{AppDir}\ipConfig\ipConfig.txt";

        public static string apiKeyDir = $@"{AppDir}\apiKey\";
        public static string apiKeyFile = $@"{AppDir}\apiKey\apiKey.txt";

        public static string ErrorLog = $@"{AppData}\WVA Compulink Integration\Errors\";
    }
}
