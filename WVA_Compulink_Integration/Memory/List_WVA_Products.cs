using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Views;

namespace WVA_Compulink_Integration.Memory
{
    class List_WVA_Products
    {
        public static List<Product> ListProducts; 

        private static ProductIn ListProductsObject = WVA_Products.GetProducts();

        public static bool LoadProducts()
        {
            if (ListProductsObject != null)
            {
                ListProducts = ListProductsObject.Products;
                return true;
            }
            else
            {
                return false;
            }                   
        }
    }
}
