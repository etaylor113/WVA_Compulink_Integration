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
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Error
{
    class AppError
    {      
        public static void ReportOrWrite(Exception e)
        {
            JsonError error = new JsonError()
            {
                ActNum = UserData.Data?.Account,
                Error = e.ToString(),
                Application = "CDI",
                AppVersion = AssemblyName.GetAssemblyName(Paths.MainAppEXE).Version.ToString()
            };

            if (!ErrorReported(error))
                ReportOrWrite(error.Error);
        }

        private static bool ErrorReported(JsonError error)
        {
            try
            {
                string dsn = UserData.Data?.DSN;

                if (dsn == null || dsn.Trim() == "")
                    return false;

                string endpoint = $"http://{dsn}/api/error/";
                string strResponse = API.Post(endpoint, error);
                bool messageSent = JsonConvert.DeserializeObject<bool>(strResponse);

                return messageSent;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void ReportOrWrite(string exceptionMessage)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                if (!Directory.Exists(Paths.ErrorLog))
                {
                    Directory.CreateDirectory(Paths.ErrorLog);
                }

                if (!File.Exists(Paths.ErrorLog + $@"\Error_{time}.txt"))
                {
                    var file = File.Create(Paths.ErrorLog + $@"\Error_{time}.txt");
                    file.Close();
                }

                using (StreamWriter writer = new StreamWriter((Paths.ErrorLog + $@"\Error_{time}.txt"), true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------------");
                    writer.WriteLine("");
                    writer.WriteLine($"(ERROR.TIME_ENCOUNTERED: {time})");
                    writer.WriteLine($"(ERROR.MESSAGE: {exceptionMessage})");
                    writer.WriteLine("");
                    writer.WriteLine("-----------------------------------------------------------------------------------");
                    writer.Close();
                }
            }
            catch { }
        }
    }
}
