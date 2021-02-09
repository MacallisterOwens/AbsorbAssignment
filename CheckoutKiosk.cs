using System;
using System.Collections.Generic;
using System.Text;

namespace AbsorbAssignment
{
    /// <summary>
    /// This class represents the checkout kiosk for GroceryCO. It begins the transaction and prints the results.
    /// </summary>
    class CheckoutKiosk
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Error: Unexpected input.\nExpected arguments: {pathToCustomerBasketFile} {pathToCurrentPriceCatalog} {pathToSalePriceCatalog}");
                throw new ArgumentException();
            }

            var basket = new CustomerBasket(args[0]).GetCustomerBasket();
            PriceCatalogs catalogs;
            Dictionary<string, float> currPriceCatalog;
            Dictionary<string, float> salePriceCatalog;

            if (args.Length == 3)
            {
                catalogs = new PriceCatalogs(args[1], args[2]);
                currPriceCatalog = catalogs.GetCurrPriceCatalog();
                salePriceCatalog = catalogs.GetSalePriceCatalog();
            }
            else
            {
                catalogs = new PriceCatalogs(args[1]);
                currPriceCatalog = catalogs.GetCurrPriceCatalog();
                salePriceCatalog = new Dictionary<string, float>();
            }

            foreach (var item in basket)
            {
                if (!currPriceCatalog.ContainsKey(item.Key))
                {
                    Console.WriteLine($"Error: {item.Key} does not exist in the current catalog.");
                    throw new Exception();
                }
            }

            float totalCost = 0.0f;
            float totalDiscount = 0.0f;

            StringBuilder output = new StringBuilder();

            output.AppendFormat(
                "{0, -25} {1, -14} {2, -14} {3, -14}\n", "Item", "Reg. Price", "Sale Price", "Discount");
            int width = output.Length;
            output.Append($"{"".PadRight(width, '-')}\n");

            foreach (var item in basket)
            {
                float regPrice = currPriceCatalog[item.Key];
                float salePrice = 0.0f;
                bool onSale = salePriceCatalog.TryGetValue(item.Key, out salePrice);
                string salePriceString = "";
                float moneySaved = regPrice - salePrice;
                string moneySavedString = "";

                if (onSale)
                {
                    salePriceString = $"{salePrice:C}";
                    moneySavedString = $"-{moneySaved:C}";
                    totalCost += salePrice * item.Value;
                    totalDiscount += moneySaved * item.Value;
                }
                else
                {
                    totalCost += regPrice * item.Value;
                }

                for (int i = 0; i < item.Value; i++)
                {
                    output.AppendFormat(
                        $"{item.Key,-25} {regPrice,-14:C} {salePriceString,-14} {moneySavedString,-14}\n");
                }
            }

            output.Append($"{"".PadRight(width, '-')}\n");

            if (totalDiscount != 0)
                output.AppendFormat("{0, -25} {1, -14:C}\n", "TOTAL SAVED", totalDiscount);

            output.AppendFormat("{0, -25} {1, -14:C}", "TOTAL PURCHASE", totalCost);

            Console.WriteLine(output);
        }
    }
}
