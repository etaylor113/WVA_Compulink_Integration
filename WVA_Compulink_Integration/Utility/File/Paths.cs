using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Utility.File
{
    class Paths
    {
        public static string ProgramFiles
        {
            get
            {
                if (Environment.Is64BitOperatingSystem)
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                else
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
            set { ProgramFiles = value; }
        }

        public static string AppData                =   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);       
        public static string PublicDocs             =   Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        public static string Desktop                =   Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string MainAppEXE             =   $@"{ProgramFiles}\WVA Compulink Integration\WVA_Compulink_Integration.exe";
        public static string LauncherAppEXE         =   $@"{ProgramFiles}\WVA Compulink Integration\WVA_CDI_Updater.exe";

        public static string DownloadLauncherUrl    =   @"https://" + $"ws2.wisvis.com/aws/compulinkIntegration/CDI/WVA_CDI_Launcher.msi";
        public static string DownloadDbUrl          =   @"https://" + $"ws2.wisvis.com/aws/compulinkIntegration/CDI/ProductPrediction.sqlite";

        public static string ResourcesDir           =   $@"{ProgramFiles}\WVA Compulink Server Integration\Resources\";
        public static string ShortcutIcon           =   $@"{ResourcesDir}\logo_plain_vector_72_white_TMD_icon.ico";

        //  PUBLIC DOCUMENTS
        public static string DSNDir                 =   $@"{PublicDocs}\WVA Compulink Integration\IpConfig\";
        public static string DSNFile                =   $@"{PublicDocs}\WVA Compulink Integration\IpConfig\IpConfig.txt";

        public static string ApiKeyDir              =   $@"{PublicDocs}\WVA Compulink Integration\ApiKey\";
        public static string ApiKeyFile             =   $@"{PublicDocs}\WVA Compulink Integration\ApiKey\ApiKey.txt";

        public static string TempDir                =   $@"{PublicDocs}\WVA Compulink Integration\Temp";
        public static string DownloadMainAppName    =   $@"{TempDir}\WVA_CDI_Setup.msi";
        public static string DownloadLauncherName   =   $@"{TempDir}\WVA_CDI_Launcher.msi";

        public static string ProductDatabaseDir     =   $@"{PublicDocs}\WVA Compulink Integration\Data";
        public static string ProductDatabaseFile    =   $@"{ProductDatabaseDir}\ProductPrediction.sqlite";

        // APP DATA
        public static string ActNumDir              =   $@"{AppData}\WVA Compulink Integration\ActNum\";
        public static string ActNumFile             =   $@"{AppData}\WVA Compulink Integration\ActNum\ActNum.txt";

        public static string ErrorLog               =   $@"{AppData}\WVA Compulink Integration\ErrorLog\";
    }
}
