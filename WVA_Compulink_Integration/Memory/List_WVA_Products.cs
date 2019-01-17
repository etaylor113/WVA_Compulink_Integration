using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WVA_Compulink_Integration.MatchFinder;

namespace WVA_Compulink_Integration.Models.Product
{
    class List_WVA_Products
    {
        public static List<Product> ListProducts; 

        private static ProductIn.ProductIn ListProductsObject = JsonConvert.DeserializeObject<ProductIn.ProductIn>(WVA_Products.GetProducts());

        public static void LoadProducts()
        {
            ListProducts = ListProductsObject.Products;           
        }
    }
}
