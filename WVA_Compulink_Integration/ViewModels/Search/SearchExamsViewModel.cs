using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;

namespace WVA_Compulink_Integration.ViewModels.Search
{
    public class SearchExamsViewModel
    {
        public SearchExamsViewModel()
        {

        }

        public static string GetExamsAsync(string date)
        {
            string endpoint = "http://10.1.4.66:44354/api/exam/date/" + date;
            return API.Get(endpoint, out string httpStatus);
        }
    }
}
