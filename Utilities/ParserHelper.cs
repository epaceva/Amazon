using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Amazon.Utilities
{
    public static class ParserHelper
    {
        public static decimal ParsePrice(string priceText)
        {
            if (string.IsNullOrEmpty(priceText))
                return 0.0m;

            string clean = Regex.Replace(priceText, "[^0-9.,]", "");

            clean = clean.Replace(",", ".");

            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            throw new Exception($"Cannot parse price value: {priceText}");
        }
    }
}