using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.MatchFinder.ProductPredictions
{
    class Database
    {

        public static void SetUpDatabase()
        {
            try
            {
                // Create the path to the product database file if it doesn't exists already
                if (!Directory.Exists($"{Paths.ProductDatabaseDir}"))
                    Directory.CreateDirectory($"{Paths.ProductDatabaseDir}");

                // Create product database file if it's not created already and set up table
                if (!File.Exists($"{Paths.ProductDatabaseFile}"))
                {
                    SQLiteConnection.CreateFile($"{Paths.ProductDatabaseFile}");
                    CreateProductTable();
                }
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private static void CreateProductTable()
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string sql = "CREATE TABLE products (" +
                                    "id                     INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "compulink_product      TEXT, " +
                                    "wva_product            TEXT, " +
                                    "num_picks              INT); ";

                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        public static string ReturnWvaProductFor(string compulinkProduct)
        {
            try
            {
                // Update order status to 'submitted'
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string query = $"SELECT wva_product FROM products WHERE compulink_product = '{compulinkProduct}'";

                SQLiteCommand command = new SQLiteCommand(query, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                string wvaProduct = null;

                while (reader.Read())
                {
                    wvaProduct = (string)reader["wva_product"];
                }

                return wvaProduct;
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return null;
            }
        }

        public static bool CompulinkProductExists(string compulinkProduct)
        {
            try
            {
                // Update order status to 'submitted'
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string query = $"SELECT compulink_product FROM products WHERE compulink_product = '{compulinkProduct}'";

                SQLiteCommand command = new SQLiteCommand(query, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                string product = null;

                while (reader.Read())
                {
                    product = (string)reader["compulink_product"];
                }

                if (product == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return false;
            }
        }

        public static bool ProductMatchExists(string compulinkProduct, string wvaProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string sql = $"SELECT compulink_product FROM products WHERE compulink_product = '{compulinkProduct}' AND wva_product = '{wvaProduct}'";

                SQLiteDataReader reader = new SQLiteCommand(sql, dbConnection).ExecuteReader();

                string readCompProd = null;

                while (reader.Read())
                    readCompProd = (string)reader["compulink_product"];

                return readCompProd != null ? true : false;
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return false;
            }
        }

        public static int GetNumPicks(string compulinkProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string query;

                if (compulinkProduct != null)
                {
                    query = $"SELECT num_picks FROM products WHERE compulink_product = '{compulinkProduct}'";
                }
                else
                {
                    throw new Exception("Invalid parameter input!");
                }

                SQLiteCommand command = new SQLiteCommand(query, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                int numPicks = 0;

                while (reader.Read())
                {
                    numPicks = (int)reader["num_picks"];
                }

                return numPicks;
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return 0;
            }
        }

        public static void IncrementNumPicks(string compulinkProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string updateOrder = $"UPDATE products SET num_picks = '{GetNumPicks(compulinkProduct: compulinkProduct) + 1}' WHERE compulink_product = '{compulinkProduct}'";

                SQLiteCommand command_1 = new SQLiteCommand(updateOrder, dbConnection);
                command_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        public static void DecrementNumPicks(string compulinkProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string updateOrder = $"UPDATE products SET num_picks = '{GetNumPicks(compulinkProduct: compulinkProduct) - 1}' WHERE compulink_product = '{compulinkProduct}'";

                SQLiteCommand command_1 = new SQLiteCommand(updateOrder, dbConnection);
                command_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        public static void UpdateCompulinkProductMatch(string compulinkProduct, string wvaProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string query = $"UPDATE products " +
                                $"SET wva_product = '{wvaProduct}' " +
                                $"WHERE compulink_product = '{compulinkProduct}'";

                SQLiteCommand command_1 = new SQLiteCommand(query, dbConnection);
                command_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        public static void CreateCompulinkProduct(string compulinkProduct, string wvaProduct)
        {
            try
            {
                SQLiteConnection dbConnection = GetSQLiteConnection();
                dbConnection.Open();

                string query = "INSERT OR IGNORE into products (" +
                                        "compulink_product, " +
                                        "wva_product, " +
                                        "num_picks) " +
                                        "values (" +
                                            $"'{compulinkProduct}', " +
                                            $"'{wvaProduct}', " +
                                            $"'{1}'" +
                                            ")";

                SQLiteCommand command_1 = new SQLiteCommand(query, dbConnection);
                command_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private static SQLiteConnection GetSQLiteConnection()
        {
            try
            {
                return new SQLiteConnection($"Data Source={Paths.ProductDatabaseFile};Version=3;");
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return null;
            }
        }

    }
}
