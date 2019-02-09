using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Models.Out_Request;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Error;

namespace WVA_Compulink_Integration.MatchFinder
{
    public class WVA_Products
    {
        public static string GetProducts()
        {
            try
            {
                string dsn = UserData._User?.DSN ?? throw new NullReferenceException();
                string endpoint = $"http://{dsn}/api/product/";

                RequestOut request = new RequestOut()
                {
                    Request = new ProductOut()
                    {
                        Api_key = UserData._User.ApiKey,
                        AccountID = UserData._User.Account
                    }
                };

                return API.Post(endpoint, request);
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                return null;
            }          
        }      
    }
}
