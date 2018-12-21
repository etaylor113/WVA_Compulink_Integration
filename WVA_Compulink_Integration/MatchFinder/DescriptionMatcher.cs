using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Product;

namespace WVA_Compulink_Integration.MatchFinder
{
    class DescriptionMatcher
    {
        public static List<MatchProduct> FindMatch(string matchString, List<Product> listProducts, double minimumScore)
        {
            List<MatchProduct> listMatchProducts = new List<MatchProduct>();

            MatchProduct matchProduct = CrossRefMatch(matchString, listProducts);

            if (matchProduct != null)
                return new List<MatchProduct>() { matchProduct };
            else
                return RunMatchFinder(matchString, listProducts, minimumScore);
        }

        // ==============================================================================================================================
        // ==============================================================================================================================
        //                  Main Functions
        // ==============================================================================================================================
        // ==============================================================================================================================

        // Will attempt to locate passed in matchString product in the cross-ref dictionary
        // If found, return match, this will return null
        private static MatchProduct CrossRefMatch(string matchString, List<Product> listProducts)
        {
            var productDict = ProductCrossRefDict.GetCrossRefDict();

            foreach (KeyValuePair<string, string> product in productDict)
            {
                if (product.Key == matchString.Trim())
                    return new MatchProduct(product.Value, 100.00) { ProductKey = GetProductKey(product.Value, listProducts) };
            }

            return null;
        }

        // Will return a list of possible matches. The lowest index will have the highest match rating
        private static List<MatchProduct> RunMatchFinder(string compareString, List<Product> listProducts, double minimumScore)
        {
            List<MatchProduct> ListMatchProducts = new List<MatchProduct>();

            foreach (Product product in listProducts)
            {
                string ourProduct = product.Description;
                string stringToCompare = compareString;

                // Changing the order of this can greatly affect the outcome of your matches
                ourProduct        =   RemoveGarbage(ourProduct);
                stringToCompare   =   RemoveGarbage(stringToCompare);
                stringToCompare   =   AdjustQuantity(stringToCompare);
                stringToCompare   =   AdjustProductName(stringToCompare);
                ourProduct        =   AdjustSKUType(ourProduct);
                stringToCompare   =   AdjustSKUType(stringToCompare);
                stringToCompare   =   BoostDullProducts(stringToCompare);

                // Get product match score
                double charDescScore    =   CompareDescriptionCharacters(ourProduct, stringToCompare);
                double quantityScore    =   DoQuantitiesMatch(ourProduct, stringToCompare);
                double skuTypeScore     =   DoSKUTypesMatch(ourProduct, stringToCompare);
                double sameWordScore    =   CheckForSameWords(ourProduct, stringToCompare);
                double totalScore       =   charDescScore + quantityScore + skuTypeScore + sameWordScore;

                // If the product score meets the minimum score defined by the caller, add it to list of possible matches
                if (totalScore > minimumScore)
                {            
                    if (!ListMatchProducts.Exists(x => x.Name == product.Description))
                        ListMatchProducts.Add(new MatchProduct(product.Description, totalScore) { ProductKey = product.ProductKey });
                }
            }

            // This just puts the matched items in a neat order so that the highest match is at the lowest index, i.e. the 100% match is at index[0] and the 98% match is at index[1]
            List<MatchProduct> SortedListProducts = ListMatchProducts.OrderBy(o => o.MatchScore).Reverse().ToList();

            return SortedListProducts;
        }

        // ==============================================================================================================================
        // ==============================================================================================================================
        //                  Helper Functions
        // ==============================================================================================================================
        // ==============================================================================================================================

        static string GetProductKey(string productName, List<Product> listProducts)
        {
            foreach (Product prod in listProducts)
            {
                if (productName == prod.Description)
                    return prod.ProductKey;
            }

            return null;
        }

        static string RemoveGarbage(string originalString)
        {
            originalString = originalString.ToLower();
            originalString = originalString.Trim();

            Regex regex0 = new Regex("trial", RegexOptions.IgnoreCase);
            MatchCollection matches0 = regex0.Matches(originalString);
            if (matches0.Count > 1)
                originalString = ReplaceLastOccurrence(originalString, "trials", "");

            Regex regex1 = new Regex("trial", RegexOptions.IgnoreCase);
            MatchCollection matches1 = regex1.Matches(originalString);
            if (matches1.Count > 1)
                originalString = ReplaceLastOccurrence(originalString, "trial", "");

            originalString = originalString.Replace("........", "")
                                           .Replace("*", "")
                                           .Replace("-", "")
                                           .Replace("?", "")
                                           .Replace(",", "");

            originalString = originalString.Replace("1d ", "1 day ")
                                           .Replace("1 d ", "1 day ")
                                           .Replace("1 da ", "1 day ")
                                           .Replace("1day ", "1 day ")
                                           // Changed .Replace("pk","pack") with .Replace("pk",""). No longer correcting 'pack' just removing it from comparison
                                           .Replace("pk", "")
                                           .Replace("pak", "")
                                           .Replace("pack", "")
                                           .Replace(" trail", " trial")
                                           .Replace("final", "")
                                           .Replace(" fina", "")
                                           .Replace(" fin", "")
                                           .Replace(" fin", "")
                                           .Replace(" fi", "");

            return originalString;
        }

        static string AdjustQuantity(string originalString)
        {
            originalString = originalString.Replace("ninety", "90")
                                           .Replace("thirty", "30")
                                           .Replace("twentyfour", "24")
                                           .Replace("twelve", "12")
                                           .Replace("six", "6");

            return originalString;
        }

        static string AdjustSKUType(string originalString)
        {
            originalString = originalString.Replace("astig ", " astigmatism ")
                                           .Replace("asti ", " astigmatism ")
                                           .Replace("astg ", " astigmatism ")
                                           .Replace("toric", " astigmatism ")
                                           .Replace("tor ", " astigmatism ")
                                           .Replace("tori ", " astigmatism ")
                                           .Replace(" mf", " multifocal ")
                                           .Replace("multif ", " multifocal ")
                                           .Replace("multi ", " multifocal ")
                                           .Replace("presby ", "presbyopia ")
                                           .Replace("asph ", " aspheric ")
                                           .Replace("clr ", " color ");

            return originalString;
        }

        static string AdjustProductName(string originalString)
        {
            originalString = originalString.Replace("av ", "acuvue ")
                                           .Replace("av2 ", "acuvue 2")
                                           .Replace("hydra ", "hydraglyde ")
                                           .Replace("frlk ", "freshlook ")
                                           .Replace("freq ", "frequency ")
                                           .Replace("prclr ", "proclear ")
                                           .Replace("pv2 ", "purevision 2 ")
                                           .Replace("air op ", "air optix ")
                                           .Replace("air opt ", "air optix ")
                                           .Replace("nt&day ", "night & day ")
                                           .Replace("aqcomfplusdali", "aqua comfort plus dailies ")
                                           .Replace("aqcmplus", "aqua comfort plus dailies ")
                                           .Replace("Aq Comf Dailies", "aqua comfort plus dailies ");

            return originalString;
        }

        static string BoostDullProducts(string originalString)
        {
            if (originalString.Contains("vitality") && !originalString.Contains("avaira"))
                originalString = originalString.Insert(0, "avaira ");
            if (originalString.Contains("define") && !originalString.Contains("lacreon"))
                originalString = originalString.Insert(0, "w/ lacreon ");
            if (originalString.Contains("biofinity t") && !originalString.Contains("tor"))
                originalString = originalString.Replace("biofinity t", "biofinity astigmatism");

            return originalString;
        }

        static double DoQuantitiesMatch(string a, string b)
        {
            bool quantitiesMatch = false;

            if (a.Contains("90") && b.Contains("90"))
                quantitiesMatch = true;
            else if (a.Contains("30") && b.Contains("30"))
                quantitiesMatch = true;
            else if (a.Contains("24") && b.Contains("24"))
                quantitiesMatch = true;
            else if (a.Contains("12") && b.Contains("12"))
                quantitiesMatch = true;
            else if ((a.Contains("6") && !a.Contains("trial")) && (b.Contains("6") && !b.Contains("trial")))
                quantitiesMatch = true;
            else if (a.Contains("trial") && b.Contains("trial"))
                quantitiesMatch = true;

            if (quantitiesMatch)
                return 25;
            else
                return 12.5;
        }

        static double DoSKUTypesMatch(string a, string b)
        {
            string a_skuType = "";
            string b_skuType = "";

            if (a.Contains("astigmatism"))
                a_skuType = "astigmatism";
            else if (a.Contains("multifocal"))
                a_skuType = "multifocal";
            else if (a.Contains("presbyopia"))
                a_skuType = "presbyopia";
            else if (a.Contains("aspheric"))
                a_skuType = "aspheric";
            else if (a.Contains("color") || a.Contains("colors"))
                a_skuType = "color";
            else if (a.Contains("trial") && b.Contains("trial"))
                a_skuType = "trial";
            else
                a_skuType = "no type";

            if (b.Contains("astigmatism"))
                b_skuType = "astigmatism";
            else if (b.Contains("multifocal"))
                b_skuType = "multifocal";
            else if (b.Contains("presbyopia"))
                b_skuType = "presbyopia";
            else if (b.Contains("aspheric"))
                b_skuType = "aspheric";
            else if (b.Contains("color") || a.Contains("colors"))
                b_skuType = "color";
            else if (b.Contains("trial") && b.Contains("trial"))
                b_skuType = "trial";
            else
                b_skuType = "no type";

            if (a_skuType == b_skuType)
                return 25;
            else
                return 12.5;
        }

        static double CheckForSameWords(string a, string b)
        {
            List<string> a_Words = a.Split(' ').ToList();
            List<string> b_Words = b.Split(' ').ToList();

            // Remove spaces to improve match score
            a_Words.RemoveAll(x => x.Equals(""));
            b_Words.RemoveAll(x => x.Equals(""));

            List<string> sharedWords = a_Words.Intersect(b_Words).ToList();

            return (10 * ((double)sharedWords.Count() / a_Words.Count()));
        }

        static double CompareDescriptionCharacters(string a, string b)
        {
            string orig_a = a;
            List<char> a_CharList = new List<char>();
            List<char> b_CharList = new List<char>();
            List<char> tempList = new List<char>();
            List<char> a_Shared = new List<char>();
            List<char> b_Shared = new List<char>();

            // Convert to a char list, it's easier to work with
            a_CharList = a.ToCharArray().ToList();
            b_CharList = b.ToCharArray().ToList();

            // Remove spaces to improve match score
            a_CharList.RemoveAll(x => x.Equals(' '));
            b_CharList.RemoveAll(x => x.Equals(' '));

            // Check for common characters from a-list to b-list
            tempList.AddRange(b_CharList);
            foreach (char character in a_CharList)
            {
                if (tempList.Contains(character))
                {
                    a_Shared.Add(character);
                    tempList.Remove(character);
                }
            }
            tempList.Clear();

            // Check for common characters from b-list to a-list
            tempList.AddRange(a_CharList);
            foreach (char character in b_CharList)
            {
                if (tempList.Contains(character))
                {
                    b_Shared.Add(character);
                    tempList.Remove(character);
                }
            }

            double a_percentMatch = (double)a_Shared.Count / b_CharList.Count();
            double b_PercentMatch = (double)b_Shared.Count / a_CharList.Count();

            return (40 * ((a_percentMatch + b_PercentMatch) / 2));
        }

        public static string ReplaceLastOccurrence(string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);

            if (place == -1)
                return source;

            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }
    }
}
