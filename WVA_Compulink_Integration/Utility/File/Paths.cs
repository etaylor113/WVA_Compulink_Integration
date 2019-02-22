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
        public static string PublicDocs = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string AppDir = Environment.CurrentDirectory;

        public static string ResourcesDir = $@"{AppDir}\Resources\";
        public static string ShortcutIcon = $@"{ResourcesDir}\logo_plain_vector_72_white_TMD_icon.ico";

        //  PUBLIC DOCUMENTS
        public static string DSNDir   = $@"{PublicDocs}\WVA Compulink Integration\IpConfig\";
        public static string DSNFile  = $@"{PublicDocs}\WVA Compulink Integration\IpConfig\IpConfig.txt";

        public static string ApiKeyDir  = $@"{PublicDocs}\WVA Compulink Integration\ApiKey\";
        public static string ApiKeyFile = $@"{PublicDocs}\WVA Compulink Integration\ApiKey\ApiKey.txt";

        public static string TempDir = $@"{PublicDocs}\Temp";

        // APP DATA
        public static string ActNumDir = $@"{AppData}\WVA Compulink Integration\ActNum\";
        public static string ActNumFile = $@"{AppData}\WVA Compulink Integration\ActNum\ActNum.txt";

        public static string ErrorLog   = $@"{AppData}\WVA Compulink Integration\ErrorLog\";
    }
}
