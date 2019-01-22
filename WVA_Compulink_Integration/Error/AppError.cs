using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Error
{
    class AppError
    {      
        public static void PrintToLog(string exceptionMessage)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                if (!Directory.Exists(Paths.ErrorLog))
                {
                    Directory.CreateDirectory(Paths.ErrorLog);
                }

                if (!File.Exists(Paths.ErrorLog + @"\Error_" + time + ".txt"))
                {
                    var file = File.Create(Paths.ErrorLog + @"\Error_" + time + ".txt");
                    file.Close();
                }

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter((Paths.ErrorLog + @"\ErrorLog.txt"), true))
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
            catch (Exception x)
            {
                Trace.WriteLine(x.Message);
            };
        }

        public static void PrintToLog(Exception exception)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                if (!Directory.Exists(Paths.ErrorLog))
                {
                    Directory.CreateDirectory(Paths.ErrorLog);
                }
                 
                if (!File.Exists(Paths.ErrorLog + @"\Error_" + time + ".txt"))
                {
                    var file = File.Create(Paths.ErrorLog + @"\Error_" + time + ".txt");
                    file.Close();
                }
              
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter((Paths.ErrorLog + @"\ErrorLog.txt"), true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------------");
                    writer.WriteLine("");
                    writer.WriteLine($"(ERROR.TIME_ENCOUNTERED: {time})");
                    writer.WriteLine($"(ERROR.MESSAGE: {exception.Message})");
                    writer.WriteLine($"(ERROR.INNER_EXCEPTION: {exception.InnerException})");
                    writer.WriteLine($"(ERROR.SOURCE: {exception.Source})");
                    writer.WriteLine("");
                    writer.WriteLine("-----------------------------------------------------------------------------------");
                    writer.Close();
                }
            }
            catch (Exception x)
            {
                Trace.WriteLine(x.Message);
            };
        }
    }
}
