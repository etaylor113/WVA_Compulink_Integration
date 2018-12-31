using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Utility.File
{
    class Paths
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string Program_x86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string ErrorLog = (@"C:\Program Files (x86)\WVA_Compulink_Integration\Errors\");
    }
}
