﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace WVA_Compulink_Integration.Cryptography
{
    class Crypto
    {
        public static string ConvertToHash(string inputString)
        {                    
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Split the input string into a character array
                char[] letterArray = inputString.ToCharArray();

                // Create a salt based on the scrambled first 6 indexes of the inputString
                string salt = letterArray[0].ToString() + letterArray[4].ToString() + letterArray[2].ToString() + letterArray[5].ToString() + letterArray[1].ToString() + letterArray[3].ToString();

                // Combine the salt and inputString to create the first hash that will be used to create the final hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(salt + inputString));

                // Build the hash string from the 'bytes' array
                StringBuilder originalHash = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    originalHash.Append(bytes[i].ToString("x2"));

                // Define a string that is a chunk of the 'originalHash' based on the length of the input string
                // NOTE: Password text field's max length is 20, so the inputString's length + 40 will never exceed the hash's max length
                string hashChunk = originalHash.ToString().Substring(inputString.Length, inputString.Length + 40);

                // Create the final hash
                bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(salt + hashChunk));

                // Build the hash string
                StringBuilder finalHash = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    finalHash.Append(bytes[i].ToString("x2"));

                // Return the hash
                return finalHash.ToString();
            }
        }       
    }
}
