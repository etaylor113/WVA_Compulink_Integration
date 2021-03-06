﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Utility.Files;

namespace WVA_Compulink_Integration.Utility.Actions
{
    class ActionLogger
    {
        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- LOGGING ACTIONS ---------------------------------------------------
        // -----------------------------------------------------------------------------------------------------

        public static void Log(string actionLocation, string actionMessage = null)
        {
            try
            {
                CreateLogFile();
                string file = GetLogFileName();

                string contents = actionMessage == null ? GetFileContents(actionLocation) : GetFileContents(actionLocation, actionMessage);

                WriteToLogFile(file, contents);
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
            }
        }

        private static void CreateLogFile()
        {
            string file = GetLogFileName();

            if (!Directory.Exists(Paths.TempDir))
                Directory.CreateDirectory(Paths.TempDir);

            if (!File.Exists(file))
                File.Create(file).Close(); ;
        }

        private static string GetLogFileName()
        {
            return $"{Paths.TempDir}CDI_Action_Log_{DateTime.Today.ToString("MM-dd-yy")}.txt";
        }

        private static string GetFileContents(string actionLocation)
        {
            try
            {
                string time = DateTime.Now.ToString("hh:mm:ss");
                return $"ApiKey={UserData.Data?.ApiKey} => MachName={Environment.MachineName} => EnvUserName={Environment.UserName} => AppUserName={UserData.Data?.UserName} => ActNum={UserData.Data?.Account} => {time} => {actionLocation}";
            }
            catch
            {
                return "";
            }
        }

        private static string GetFileContents(string actionLocation, string actionMessage)
        {
            return GetFileContents(actionLocation) + $" => {actionMessage}";
        }

        private static void WriteToLogFile(string file, string contents)
        {
            try
            {
                if (!File.Exists(file))
                    File.Create(file);

                var stream = File.AppendText(file);
                stream.WriteLine(contents);
                stream.Close();
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
            }
        }

        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- GETTING ACTION DATA -----------------------------------------------
        // -----------------------------------------------------------------------------------------------------

        public static List<ActionData> GetData()
        {
            try
            {
                List<ActionData> listActionData = new List<ActionData>();

                var files = Directory.EnumerateFiles(Paths.TempDir, "CDI_Action_Log*").Where(x => !x.Contains(DateTime.Today.ToString("MM-dd-yy")));

                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        string content = File.ReadAllText(file);
                        listActionData.Add(new ActionData(file, content));
                    }
                }

                return listActionData;
            }
            catch
            {
                return null;
            }
        }

        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- SENDING ACTION DATA -----------------------------------------------
        // -----------------------------------------------------------------------------------------------------

        public static bool ReportData(string data)
        {
            try
            {
                var dataMessage = new JsonError()
                {
                    ActNum = UserData.Data.Account,
                    Error = data.ToString(),
                    Application = "CDI-DAL",
                    AppVersion = AssemblyName.GetAssemblyName(Paths.MainAppEXE).Version.ToString()
                };

                string strResponse = API.Post(Paths.WisVisErrors, dataMessage);
                var response = JsonConvert.DeserializeObject<Response>(strResponse);

                return response.Status == "SUCCESS" ? true : false;
            }
            catch
            {
                return false;
            }
        }

    }
}
