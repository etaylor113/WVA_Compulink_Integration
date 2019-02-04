using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Models.Out_Request;
using WVA_Compulink_Integration.Memory;

namespace WVA_Compulink_Integration.MatchFinder
{
    public class WVA_Products
    {
        public static string GetProducts()
        {
            string endpoint = "https://orders-qa.wisvis.com/products";

            ProductOut productOut = new ProductOut()
            {
                Api_key = UserData._User.ApiKey,
                AccountID = "44"           
            };

            Out_Request request = new Out_Request()
            {
                Request = productOut

            };

            return API.Post(endpoint, request);
        }
       
    }
}
