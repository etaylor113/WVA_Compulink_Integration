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
        
        public static string ActNumDir  = $@"{AppData}\WVA Compulink Integration\ActNum\";
        public static string ActNumFile = $@"{AppData}\WVA Compulink Integration\ActNum\ActNum.txt";

        public static string DSNDir   = $@"{AppData}\WVA Compulink Integration\IpConfig\";
        public static string DSNFile  = $@"{AppData}\WVA Compulink Integration\IpConfig\IpConfig.txt";

        public static string apiKeyDir  = $@"{AppData}\WVA Compulink Integration\ApiKey\";
        public static string apiKeyFile = $@"{AppData}\WVA Compulink Integration\ApiKey\ApiKey.txt";

        public static string ErrorLog   = $@"{AppData}\WVA Compulink Integration\ErrorLog\";
    }
}
