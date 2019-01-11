using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Models.User
{
    class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
        public string Location { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string ApiKey { get; set; }
        public string Status { get; set; }  
        public string Message { get; set; }
    }
}
