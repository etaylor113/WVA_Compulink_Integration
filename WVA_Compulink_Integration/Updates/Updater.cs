using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Utility.Files;

namespace WVA_Compulink_Integration.Updates
{
    class Updater
    {

        public async static void RunLaucherUpdate()
        {
            try
            {
                if (!File.Exists(Paths.LauncherAppEXE) || UpdateAvailable("CDI_Launcher"))
                {
                    // Get the update file
                    GetUpdateFile(Paths.DownloadLauncherUrl, Paths.DownloadLauncherName);

                    // Install the update and wait for it to complete
                    Task updateTask = Task.Factory.StartNew(() =>
                    {
                        UpdateApp();
                    });

                    updateTask.Wait();
                }
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        // Scans assembly file and checks if there is a newer version available
        private static bool UpdateAvailable(string program)
        {
            try
            {
                // CDI_Launcher is the target application
                var updateOutput = new UpdateOutput()
                {
                    Version = AssemblyName.GetAssemblyName(Paths.LauncherAppEXE).Version.ToString(),
                    Program = program,
                };
                string endpoint = "https://ws2.wisvis.com/aws/scanner/json_check_update.rb";
                string updateResponse = API.Post(endpoint, updateOutput);
                var response = JsonConvert.DeserializeObject<Response>(updateResponse);

                if (response.Status == "SUCCESS")
                {
                    if (response.Message == "True")
                        return true;
                    else
                        return false;
                }
                else
                    throw new Exception("Response: FAIL from 'updateResponse' in UpdateAvailable()");
            }
            catch
            {
                return false;
            }
        }

        private static void GetUpdateFile(string downloadLocationPath, string pathToDownload)
        {
            try
            {
                using (var client = new WebClient())
                {
                    // Make sure TempDir is available
                    if (!Directory.Exists(Paths.TempDir))
                        Directory.CreateDirectory(Paths.TempDir);

                    client.DownloadFile(downloadLocationPath, pathToDownload);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve update file. Error: {ex.Message}");
            }
        }

        private async static void UpdateApp()
        {
            try
            {
                // Double check that update file is in Temp
                if (!File.Exists(Paths.DownloadMainAppName))
                    throw new FileNotFoundException("Error: WVA_CDI_Launcher.msi not found at path 'C:\\Users\\Public\\Documents\\WVA Compulink Integration\\Temp\\'");

                // Attempt to reinstall application
                Process p = new Process();
                p.StartInfo.FileName = "msiexec.exe";
                p.StartInfo.Arguments = "/i \"C:\\Users\\Public\\Documents\\WVA Compulink Integration\\Temp\\WVA_CDI_Launcher.msi\"/passive";
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has occurred while attempting to update application. Error: {ex.Message}");
            }
        }

        private static bool DatabaseUpdateAvailable()
        {
            return UpdateAvailable("CSI_db");
        }

        private static void GetDatabaseUpdateFile()
        {
            GetUpdateFile(Paths.DownloadDbUrl, Paths.ProductDatabaseFile);
        }

        // Merges the current SQLite file data with the new one 
        private static void MergeDatabases()
        {
            // Check if downloaded sqlite file exists in /temp/
            // ATTACH database(fileName) AS updateDatabase
            // INSERT INTO updateDatabase.orders SELECT * FROM orders
            // INSERT INTO updateDatabase.order_details SELECT * FROM order_details
            // INSERT INTO updateDatabase.users SELECT * FROM users
            // DETACH database
            // Double check that data is in new database
            // Delete old database file in /Data/ and move new database file from /Temp/ to /Data/
        }



    }
}
