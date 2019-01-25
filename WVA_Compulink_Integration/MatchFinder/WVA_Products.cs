using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Models.Out_Request;

namespace WVA_Compulink_Integration.MatchFinder
{
    public class WVA_Products
    {
        public static string GetProducts()
        {
            string endpoint = "https://orders-qa.wisvis.com/products";

            ProductOut productOut = new ProductOut()
            {
                Api_key = "426761f0-3e9d-4dfd-bdbf-0f35a232c285",
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
