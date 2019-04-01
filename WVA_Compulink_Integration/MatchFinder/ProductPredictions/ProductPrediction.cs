using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Product;

namespace WVA_Compulink_Integration.MatchFinder.ProductPredictions
{
    class ProductPrediction
    {
        public static double MatchScore { get; set; }

        public static List<MatchProduct> GetPredictionMatches(string product, double matchScore, List<Product> wvaProducts, bool overrideNumPicks = false)
        {
            // Check for nulls
            if (product == null || product.Trim() == "")
                throw new Exception("string 'compulinkProduct' cannot be null or blank.");

            if (wvaProducts == null || wvaProducts?.Count < 1)
                throw new Exception("List of WVA products cannot be null or empty.");

            // Make sure database is set up
            Database.SetUpDatabase();
            MatchScore = matchScore;

            // If 'product' is a stored wva match product, leave method. There is no reason to find any matches for it.
            MatchProduct matchProduct = WvaProductExists(product, wvaProducts);
            if (matchProduct != null)
                return new List<MatchProduct>() { matchProduct };

            List<MatchProduct> listMatches = new List<MatchProduct>();

            // Get wva products that are similarly matched with the compulink product
            listMatches = GetMatches(product, wvaProducts, overrideNumPicks);

            return listMatches;
        }

        public static void LearnProduct(string compulinkProduct, string wvaProduct)
        {
            // Check for nulls
            if (compulinkProduct == null || compulinkProduct.Trim() == "")
                return;

            if (wvaProduct == null || wvaProduct.Trim() == "")
                throw new Exception("List of WVA products cannot be null or empty.");

            // Make sure database is set up 
            Database.SetUpDatabase();

            // Increase number of times this product has been picked or create a new object if it has not been used already
            if (Database.ProductMatchExists(compulinkProduct: compulinkProduct, wvaProduct: wvaProduct))
            {
                Database.IncrementNumPicks(compulinkProduct);
            }
            else if (Database.CompulinkProductExists(compulinkProduct) && Database.ReturnWvaProductFor(compulinkProduct) != wvaProduct)
            {
                int numPicks = Database.GetNumPicks(compulinkProduct);

                if (numPicks > 1)
                    Database.DecrementNumPicks(compulinkProduct);
                else
                    Database.UpdateCompulinkProductMatch(compulinkProduct, wvaProduct);
            }
            else
            {
                Database.CreateCompulinkProduct(compulinkProduct, wvaProduct);
            }
        }

        public static MatchProduct WvaProductExists(string product, List<Product> wvaProducts)
        {
            Product wvaProduct = wvaProducts.Where(x => x.Description == product).FirstOrDefault();
        
            if (wvaProduct != null)
                return new MatchProduct(wvaProduct?.Description, 100) { ProductKey = wvaProduct.ProductKey};
            else
                return null;
        }

        // Get a list of wva product matches for a given compulink product
        private static List<MatchProduct> GetMatches(string product, List<Product> wvaProducts, bool overrideNumPicks)
        {
            var listMatches = new List<MatchProduct>();

            // If overrideNumPicks is true, the method will not limit the list based on numPicks
            int numPicks;
            if (overrideNumPicks)
                numPicks = 0;
            else
                numPicks = Database.GetNumPicks(compulinkProduct: product);

            // If 10 or more numPicks only show suggested product (confidence: extremely confident)
            if (numPicks >= 10)
            {
                MatchProduct matchProduct;
                matchProduct = WvaProductExists(Database.ReturnWvaProductFor(product), wvaProducts);

                listMatches.Add(matchProduct);
                return listMatches;
            }        
            // If 7-9 numPicks show suggested product and 4 matches (high confidence)
            else if (numPicks >= 7)
            {
                listMatches = FilterList(6, product, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(product), matchScore: 100));
                return listMatches;
            }
            // If 5-6 numPicks show suggested product and 4 matches (confident)
            else if (numPicks >= 5)
            {
                listMatches = FilterList(11, product, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(product), matchScore: 100));
                return listMatches;
            }
            // If 3-4 numPicks show suggested product and 14 matches (somewhat confident)
            else if (numPicks >= 3)
            {
                listMatches = FilterList(16, product, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(product), matchScore: 100));
                return listMatches;
            }
            // If 1-2 numPicks show suggested product and all matches (low confidence)
            else if (numPicks >= 1)
            {
                listMatches = FilterList(999, product, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(product), matchScore: 100));
                return listMatches;
            }
            // If 0 numPicks show all matches (no confidence)
            else
            {
                listMatches = DescriptionMatcher.FindMatch(product, wvaProducts, MatchScore);
                return listMatches;
            }
        }

        private static List<MatchProduct> FilterList(int countLimit, string product, List<Product> wvaProducts = null, MatchProduct suggestedProduct = null)
        {
            List<MatchProduct> listMatches = new List<MatchProduct>();

            if (suggestedProduct != null)
            {
                string wvaProd = Database.ReturnWvaProductFor(product);
                listMatches.Add(new MatchProduct(name: wvaProd, matchScore: 100));

                // Find product code for  
                foreach (Product prod in wvaProducts)
                {
                    if (prod.Description == wvaProd)
                    {
                        listMatches[0].ProductKey = prod.ProductKey;
                    }
                }
            }              

            if (wvaProducts != null)
                listMatches.AddRange(DescriptionMatcher.FindMatch(product, wvaProducts, MatchScore));
          
            // Remove match product in list that is the same as the suggested product
            if (listMatches.Count > 1)
            {
                listMatches = listMatches.GroupBy(x => x.Name).Select(x => x.First()).ToList();          
            }

            // Get only top x matches
            for (int i = listMatches.Count; i > countLimit; i--)
                listMatches.RemoveAt(i - 1);

            return listMatches;
        }

    }
}
