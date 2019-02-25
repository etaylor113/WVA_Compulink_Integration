using IWshRuntimeLibrary;
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
using WVA_Compulink_Integration.Models.Updates;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Updates
{
    public static class Update
    {
        public static void PerformUpdate()
        {
            try
            {
                bool updateAvailable = UpdateAvailable();

                if (updateAvailable)
                {
                    GetUpdateFile();
                    UpdateApp();
                }
            }
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
            }
        }


        // Scans assembly file and checks if there is a newer version available
        public static bool UpdateAvailable()
        {
            var updateOutput = new UpdateOutput()
            {
                Version = AssemblyName.GetAssemblyName(Paths.RunningEXE).Version.ToString(),
                Program = "WVA_Scan_App",
                //ActNum = "99999"
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
            {
                throw new Exception("Response: FAIL from 'updateResponse' in UpdateAvailable()");
            }
        }    

        private static void GetUpdateFile()
        {
            try
            {
                using (var client = new WebClient())
                {
                    // Make sure TempDir is available
                    if (!Directory.Exists(Paths.TempDir))
                        Directory.CreateDirectory(Paths.TempDir);

                    client.DownloadFile("https://ws2.wisvis.com/aws/scanner/CurrentMSI/WVA_CDI_Setup.msi", Paths.DownloadName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get update file. Error: {ex.Message}");
            }
        }

        private static void UpdateApp()
        {
            Process p = new Process();
            p.StartInfo.FileName = "msiexec.exe";
            p.StartInfo.Arguments = "/f a \"C:\\Users\\Public\\Documents\\WVA_Scan\\Temp\\WVA_Scan_App.msi\"/passive";
            p.Start();
            p.WaitForExit();
        }    
    }
}
