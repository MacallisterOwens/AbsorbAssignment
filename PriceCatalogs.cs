using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AbsorbAssignment
{
    /// <summary>
    /// This class represents both the current price catalog and sale price catalog of GroceryCO
    /// </summary>
    class PriceCatalogs
    {
        /// <summary>
        /// A dictionary which contains a mapping from the item's name and its current price
        /// </summary>
        private Dictionary<string, float> currPriceCatalog;
        /// <summary>
        /// A dictionary which contains a mapping from the item's name and its sale price (if applicable)
        /// </summary>
        private Dictionary<string, float> salePriceCatalog;

        // Assumes that the text doc is csv with no spaces formatted as "{itemName},{price}/n{itemName},{price}/n..."

        /// <summary>Initializes a new instance of the <see cref="PriceCatalogs" /> class and populates only the current price catalog via the file path provided.</summary>
        /// <param name="currPricePath">The path to the file which the current price catalog will be read from</param>
        /// <exception cref="FileNotFoundException">Could not access/find {currPricePath}</exception>
        public PriceCatalogs(string currPricePath)
        {
            currPriceCatalog = new Dictionary<string, float>();
            salePriceCatalog = new Dictionary<string, float>();

            if (!File.Exists(currPricePath))
                throw new FileNotFoundException($"Could not access/find {currPricePath}");

            try
            {
                StreamReader reader = File.OpenText(currPricePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] vals = line.Split(',');
                    try
                    {
                        currPriceCatalog.Add(vals[0], float.Parse(vals[1], CultureInfo.InvariantCulture));
                    }
                    catch (ArgumentException e) { }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue while trying to read {currPricePath}");
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="PriceCatalogs" /> class and populates the current price catalog and the sale price catalog via the file paths provided.</summary>
        /// <param name="currPricePath">The path to the file which the current price catalog will be read from</param>
        /// <param name="salePricePath">The path to the file which the sale price catalog will be read from</param>
        /// <exception cref="FileNotFoundException">Could not access/find {currPricePath}</exception>
        public PriceCatalogs(string currPricePath, string salePricePath)
        {
            currPriceCatalog = new Dictionary<string, float>();
            salePriceCatalog = new Dictionary<string, float>();

            if (!File.Exists(currPricePath) || !File.Exists(salePricePath))
                throw new FileNotFoundException($"Could not access/find {currPricePath} or {salePricePath}");

            try
            {
                StreamReader reader = File.OpenText(currPricePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] vals = line.Split(',');
                    try
                    {
                        currPriceCatalog.Add(vals[0], float.Parse(vals[1], CultureInfo.InvariantCulture));
                    }
                    catch (ArgumentException e) { }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue while trying to read {currPricePath}");
                Console.WriteLine(e);
                throw;
            }

            try
            {
                StreamReader reader = File.OpenText(salePricePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] vals = line.Split(',');
                    try
                    {
                        salePriceCatalog.Add(vals[0], float.Parse(vals[1], CultureInfo.InvariantCulture));
                    }
                    catch (ArgumentException e) { }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue while trying to read {salePricePath}");
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>Gets the current price catalog.</summary>
        /// <returns>Returns the current price catalog</returns>
        public Dictionary<string, float> GetCurrPriceCatalog()
        {
            return currPriceCatalog;
        }

        /// <summary>Gets the sale price catalog.</summary>
        /// <returns>Returns the sale price catalog</returns>
        public Dictionary<string, float> GetSalePriceCatalog()
        {
            return salePriceCatalog;
        }

    }
}
