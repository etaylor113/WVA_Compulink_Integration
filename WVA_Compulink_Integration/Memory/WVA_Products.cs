using System;
using System.Collections.Generic;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Error;
using Newtonsoft.Json;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Models.Product;

namespace WVA_Compulink_Integration.Memory
{
    public class WvaProducts
    {
        public static List<Product> ListProducts;

        public static void LoadProductList(RequestOut request, string endpoint)
        {        
            string data = API.Post(endpoint, request);

            ProductIn productIn = JsonConvert.DeserializeObject<ProductIn>(data);

            if (productIn == null || productIn.Products == null || productIn.Products.Count < 1)
                throw new Exception("List WVA products returned null or empty.");                             
            else if (productIn.Status == "SUCCESS")
                ListProducts = productIn.Products;
            else
                throw new Exception($"Error getting WVA products. Status: {productIn.Status} -- Message: {productIn.Message}");                                             
        }      
    }
}
