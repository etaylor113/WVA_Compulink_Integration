using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Error
{
    public class ErrorOutput
    {
        public string User;
        public string Error;

        public ErrorOutput(string _User, string _Error)
        {
            User = _User;
            Error = _Error;
        }
    }
}
