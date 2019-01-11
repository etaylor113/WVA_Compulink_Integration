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
        private void Report(string error)
        {
            try
            {
                // This might be used later
                //string userName = User.GetUserName();
                // ErrorOutput errorOutput = new ErrorOutput(userName, error);

                var json = JsonConvert.SerializeObject(error);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ws2.wisvis.com/aws/scanner/error_handler.rb");
                request.Method = "POST";
                request.Timeout = 30000;

                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(json);

                request.ContentLength = byteArray.Length;
                request.ContentType = @"application/json";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var json_Message = reader.ReadToEnd();
                    var jsonResponse = JsonConvert.DeserializeObject<Response>(json_Message);

                    if (jsonResponse.Status == "SUCCESS")
                    {

                    }
                    else if (jsonResponse.Message == "FAIL")
                    {
                        PrintToLog(new Exception("An attempt was made to report an error but a failed response was encountered."));
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Invalid parameter returned from endpoint.");
                    }
                    reader.Close();
                }
                response.Close();

                if ((((HttpWebResponse)response).StatusDescription) != "OK")
                {
                    throw new System.InvalidOperationException("Attempted to connect but a connection could not be established.");
                }
            }
            catch (Exception e)
            {
                PrintToLog(e);
            }
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
