using System;
using System.IO;
using System.Collections.Generic;

namespace AbsorbAssignment
{
    /// <summary>
    /// This class represents the items a customer scans at the kiosk, i.e. the items in their basket
    /// </summary>
    class CustomerBasket
    {

        /// <summary>
        /// A dictionary which contains a mapping from the item's name and the number of times that item was scanned
        /// </summary>
        private Dictionary<string, int> itemsDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBasket"/> class and does not populate itemsDict.
        /// </summary>
        public CustomerBasket()
        {
            itemsDict = new Dictionary<string, int>();
        }

        /// <summary>Initializes a new instance of the <see cref="CustomerBasket" /> class.
        /// Populates the itemsDict with the items given in the file pointed to by fPath</summary>
        /// <param name="fPath">The path to the file containing the customer's scanned items</param>
        public CustomerBasket(string fPath)
        {
            itemsDict = new Dictionary<string, int>();
            ReadInItems(fPath);
        }

        /// <summary>
        /// Returns a dictionary representing the customer's basket.
        /// </summary>
        /// <returns>A dictionary which contains a mapping from the item's name and the number of times that item was scanned</returns>
        public Dictionary<string, int> GetCustomerBasket()
        {
            return itemsDict;
        }

        /// <summary>
        /// Reads in the file given by fPath and populates the itemsDict with the items read in.
        /// </summary>
        /// <param name="fPath">The path to the file containing the customer's scanned items.</param>
        /// <exception cref="Exception">File path given does not exist or is incorrect</exception>
        public void ReadInItems(string fPath)
        {
            if (File.Exists(fPath))
            {
                string[] items = File.ReadAllLines(fPath);
                foreach (var item in items)
                {
                    try
                    {
                        itemsDict.Add(item, 1);
                    }
                    catch (ArgumentException e)
                    {
                        itemsDict[item]++;
                    }
                }
            }
            else
            {
                throw new Exception("File path does not exist or is incorrect");
            }
        }

    }
}
