using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Updates
{
    public static class Update
    {

        // Scans assembly file and checks if there is a newer version available
        public static bool UpdateAvailable()
        {
            bool needsUpdate = true;

            return needsUpdate;
        }
    
        public static void PerformUpdate()
        {


        }

        private static void GetUpdateFile()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve update.");
            }
        }

        private static void DeleteOldShortcut()
        {
            string shortcut = $@"{Paths.Desktop}\WVA Compulink Integration.lnk";
            if (System.IO.File.Exists(shortcut))
                System.IO.File.Delete(shortcut);
        }

        private static void CreateNewShortcut(string updateFileName)
        {
            string targetFullPath = $@"{Paths.TempDir}\{updateFileName}";

            WshShell wsh = new WshShell();
            IWshShortcut shortcut = wsh.CreateShortcut($"{Paths.Desktop}\\WVA Scan App.lnk") as IWshShortcut;
            shortcut.TargetPath = $@"{targetFullPath}";
            shortcut.WindowStyle = 1;
            shortcut.Description = "WVA Compulink Integration";
            shortcut.IconLocation = $@"{Paths.ShortcutIcon}";
            shortcut.Save();

        }
    }
}
