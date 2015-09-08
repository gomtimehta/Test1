
using System.Text.RegularExpressions;

namespace Core
{
    public class Quote
    {
        public Quote(string symbol, double price, double volume)
        {
            Symbol = symbol;
            Price = price;
            Volume = volume;
        }

        public string Symbol
        {
            get;
        }

        public double Price
        {
            get;
            private set;
        }

        public double Volume
        {
            get;
            private set;
        }

        public void UpdateQuote(double price, double volume)
        {
            Price = price;
            Volume = volume;
        }

        public bool IsQuoteValid()
        {
            bool isSymbolValid = Regex.IsMatch(Symbol, @"^[A-Z]{3}$");
            bool isPriceValid = Price > 0;
            bool isVolumeValid = Volume > 0;

            return isSymbolValid && isPriceValid && isVolumeValid;
        }

    }


}
