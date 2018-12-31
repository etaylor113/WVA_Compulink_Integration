using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Error;

namespace WVA_Compulink_Integration._API
{
    class API
    {
        static public string Post(string endpoint, object jsonObject, out string httpStatus)
        {
            try
            {
                string targetResponse = null;
                string json = JsonConvert.SerializeObject(jsonObject);

                UTF8Encoding encoding = new UTF8Encoding();
                byte[] byteArray = encoding.GetBytes(json);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
                request.ContentLength = byteArray.Length;
                request.ContentType = @"application/json";
                request.Method = "POST";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse webResponse = request.GetResponse();
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    targetResponse = reader.ReadToEnd();
                    reader.Close();
                }
                httpStatus = ((HttpWebResponse)webResponse).StatusDescription.ToString();

                return targetResponse;
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                httpStatus = null;
                return "ERROR: " + x.Message;
            }
        }

        static public string Get(string endpoint, out string httpStatus)
        {
            try
            {
                string targetResponse = null;
                httpStatus = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.ContentType = @"application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return targetResponse = reader.ReadToEnd();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                httpStatus = null;
                return "ERROR: " + x.Message;
            }
        }

    }
}
