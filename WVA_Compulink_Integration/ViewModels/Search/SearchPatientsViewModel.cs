using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;

namespace WVA_Compulink_Integration.ViewModels.Search
{
    public class SearchPatientsViewModel
    {
        public SearchPatientsViewModel()
        {


        }

        public static string GetPatientsAsync()
        {
            // http://localhost:56075/CompuClient/Patients
            // http://10.1.4.66:44354/api/patient/

            string endpoint = "http://localhost:56075/CompuClient/Patients";
            return API.Get(endpoint, out string httpStatus);
        }
    }
}
