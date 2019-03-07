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

        public static List<MatchProduct> GetPredictionMatches(string compulinkProduct, double matchScore, List<Product> wvaProducts)
        {
            // Check for nulls
            if (compulinkProduct == null || compulinkProduct.Trim() == "")
                throw new Exception("string 'compulinkProduct' cannot be null or blank.");

            if (wvaProducts == null || wvaProducts?.Count < 1)
                throw new Exception("List of WVA products cannot be null or empty.");

            // Make sure database is set up
            Database.SetUpDatabase();
            MatchScore = matchScore;

            List<MatchProduct> listMatches = new List<MatchProduct>();

            // Get wva products that are similarly matched with the compulink product
            listMatches = GetMatches(compulinkProduct, wvaProducts);

            return listMatches;
        }

        public static void LearnProduct(string compulinkProduct, string wvaProduct)
        {
            // Check for nulls
            if (compulinkProduct == null || compulinkProduct.Trim() == "")
                throw new Exception("string 'compulinkProduct' cannot be null or blank.");

            if (wvaProduct == null || wvaProduct.Trim() == "")
                throw new Exception("List of WVA products cannot be null or empty.");

            // Make sure database is set up 
            Database.SetUpDatabase();

            // Check for nulls
            if (compulinkProduct == null || compulinkProduct?.Trim() == "")
                throw new Exception("'compulinkProduct' cannot be null or blank.");

            if (wvaProduct == null || wvaProduct?.Trim() == "")
                throw new Exception("'wvaProduct' cannot be null or blank");

            // Increase number of times this product has been picked or create a new object if it has not been used already
            if (Database.ProductMatchExists(compulinkProduct: compulinkProduct, wvaProduct: wvaProduct))
            {
                Trace.WriteLine($"\nIncrementing product: \n\t(Compulink: {compulinkProduct}) \n\t(WVA: {wvaProduct})");
                Database.IncrementNumPicks(compulinkProduct);
            }
            else if (Database.CompulinkProductExists(compulinkProduct) && Database.ReturnWvaProductFor(compulinkProduct) != wvaProduct)
            {
                int numPicks = Database.GetNumPicks(compulinkProduct);

                if (numPicks > 1)
                {
                    Trace.WriteLine($"\nDecrementing product: \n\t(Compulink: {compulinkProduct}) \n\t(WVA: {wvaProduct})");
                    Database.DecrementNumPicks(compulinkProduct);
                }
                else
                {
                    Trace.WriteLine($"\nUpdating compulink product ({compulinkProduct}) suggestion to {wvaProduct}");
                    Database.UpdateCompulinkProductMatch(compulinkProduct, wvaProduct);
                }
            }
            else
            {
                Trace.WriteLine($"\nAdding compulink product: \n\t(Compulink: {compulinkProduct}) \n\t(WVA: {wvaProduct})");
                Database.CreateCompulinkProduct(compulinkProduct, wvaProduct);
            }
        }

        // Get a list of wva product matches for a given compulink product
        private static List<MatchProduct> GetMatches(string compulinkProduct, List<Product> wvaProducts)
        {
            var listMatches = new List<MatchProduct>();

            int numPicks = Database.GetNumPicks(compulinkProduct: compulinkProduct);

            // If 10 or more numPicks only show suggested product (confidence: extremely confident)
            if (numPicks >= 10)
            {
                listMatches.Add(new MatchProduct(name: Database.ReturnWvaProductFor(compulinkProduct), matchScore: 100));
                return listMatches;
            }

            // If 7-9 numPicks show suggested product and 4 matches (high confidence)
            else if (numPicks >= 7)
            {
                listMatches = FilterList(6, compulinkProduct, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(compulinkProduct), matchScore: 100));
                return listMatches;
            }

            // If 5-6 numPicks show suggested product and 4 matches (confident)
            else if (numPicks >= 5)
            {
                listMatches = FilterList(11, compulinkProduct, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(compulinkProduct), matchScore: 100));
                return listMatches;
            }

            // If 3-4 numPicks show suggested product and 14 matches (somewhat confident)
            else if (numPicks >= 3)
            {
                listMatches = FilterList(16, compulinkProduct, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(compulinkProduct), matchScore: 100));
                return listMatches;
            }

            // If 1-2 numPicks show suggested product and all matches (low confidence)
            else if (numPicks >= 1)
            {
                listMatches = FilterList(999, compulinkProduct, wvaProducts, new MatchProduct(name: Database.ReturnWvaProductFor(compulinkProduct), matchScore: 100));
                return listMatches;
            }

            // If 0 numPicks show all matches (no confidence)
            else
            {
                listMatches = DescriptionMatcher.FindMatch(compulinkProduct, wvaProducts, MatchScore);
                return listMatches;
            }
        }

        private static List<MatchProduct> FilterList(int countLimit, string compulinkProduct, List<Product> wvaProducts = null, MatchProduct suggestedProduct = null)
        {
            List<MatchProduct> listMatches = new List<MatchProduct>();

            if (suggestedProduct != null)
            {
                string wvaProd = Database.ReturnWvaProductFor(compulinkProduct);
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
                listMatches.AddRange(DescriptionMatcher.FindMatch(compulinkProduct, wvaProducts, MatchScore));
          
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
