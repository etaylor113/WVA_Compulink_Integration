using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Error
{
    public class ErrorOutput
    {
        private string ActNum { get; set; }
        private string Error { get; set; }
        private string Application { get; set; }
        private string AppVersion { get; set; }

        public ErrorOutput(string actNum, string error, string application, string appVersion)
        {
            ActNum = actNum;
            Error = error;
            Application = application;
            AppVersion = appVersion;
        }
    }
}
