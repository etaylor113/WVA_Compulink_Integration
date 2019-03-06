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
using Newtonsoft.Json;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Models.Response;

namespace WVA_Compulink_Integration.MatchFinder
{
    public class WVA_Products
    {
        public static ProductIn GetProducts()
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

                string data = API.Post(endpoint, request);

                try
                {
                    if (data == null || data == "")
                        throw new NullReferenceException("'data' in WVA_Products.GetProducts() cannot be null or empty");

                    ProductIn product = JsonConvert.DeserializeObject<ProductIn>(data);

                    if (product.Status == "SUCCESS")
                        return product;
                    else
                        throw new Exception($"Exception getting WVA products. Status: {product.Status} -- Message: {product.Message}");
                }
                catch
                {
                    Response response = JsonConvert.DeserializeObject<Response>(data);

                    if (response.Status == "FAIL")
                        throw new Exception($"An exception has occurred while trying to get WVA products. Status: {response.Status} -- Message: {response.Message}");
                    else
                        return null;
                }                             
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                return null;
            }          
        }      
    }
}
