using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Patient;

namespace WVA_Compulink_Integration.Memory
{
    class ListPatients
    {
        public static List<Patient> OrigListPatients = new List<Patient>() { };
        public static List<Patient> CopyListPatients = new List<Patient>() { };
    }
}
