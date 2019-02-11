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
        
        public static string ActNumDir  = $@"{AppData}\WVA_Compulink_Integration\ActNum\";
        public static string ActNumFile = $@"{AppData}\WVA_Compulink_Integration\ActNum\ActNum.txt";

        public static string DSNDir   = $@"{AppData}\WVA_Compulink_Integration\IpConfig\";
        public static string DSNFile  = $@"{AppData}\WVA_Compulink_Integration\IpConfig\IpConfig.txt";

        public static string apiKeyDir  = $@"{AppData}\WVA_Compulink_Integration\ApiKey\";
        public static string apiKeyFile = $@"{AppData}\WVA_Compulink_Integration\ApiKey\ApiKey.txt";

        public static string ErrorLog   = $@"{AppData}\WVA_Compulink_Integration\ErrorLog\";
    }
}
