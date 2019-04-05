using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
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
                AppError.ReportOrWrite(ex);
            }
        }

        private static void CreateLogFile()
        {
            string file = GetLogFileName();

            if (!Directory.Exists(Paths.ActionLogDir))
                Directory.CreateDirectory(Paths.ActionLogDir);

            if (!File.Exists(file))
                File.Create(file).Close(); ;
        }

        private static string GetLogFileName()
        {
            return $"{Paths.ActionLogDir}CDI_Action_Log_{DateTime.Today.ToString("MM-dd-yy")}.txt";
        }

        private static string GetFileContents(string actionLocation)
        {
            string time = DateTime.Now.ToString("hh:mm:ss");
            return $"{UserData.Data.ApiKey} => {UserData.Data.UserName} => {UserData.Data.Account} => {time} => {actionLocation}";
        }

        private static string GetFileContents(string actionLocation, string actionMessage)
        {
            return GetFileContents(actionLocation) + $" => {actionMessage}";
        }

        private static void WriteToLogFile(string file, string contents)
        {
            var stream = File.AppendText(file);
            stream.WriteLine(contents);
            stream.Close();
        }

        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- DELETING OLD ACTION FILES -----------------------------------------
        // -----------------------------------------------------------------------------------------------------



        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- GETTING ACTION DATA -----------------------------------------------
        // -----------------------------------------------------------------------------------------------------

        public static List<string> GetData()
        {
            List<string> listActionData = new List<string>();

            var files = Directory.EnumerateFiles(Paths.ActionLogDir, "CDI_Action_Log*").Where(x => !x.Contains(DateTime.Today.ToString("MM-dd-yy")));

            // Exclude any files created today
            //files = files.Where(x => !x.Contains(DateTime.Today.ToString("MM-dd-yy")));
            
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    string content = File.ReadAllText(file);
                    listActionData.Add(content);
                }
            }

            return listActionData;
        }

        // -----------------------------------------------------------------------------------------------------
        // --------------------------------- SENDING ACTION DATA -----------------------------------------------
        // -----------------------------------------------------------------------------------------------------

        public static bool ReportData(string data)
        {
            return true;
        }

        public static bool ReportData(IEnumerable<string> data)
        {
            return true;
        }

    }
}
