using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication1
{
    public class QuoteWithDataValidationAnnotations
    {
        public QuoteWithDataValidationAnnotations(string symbol, double price, double volume)
        {
            Symbol = symbol;
            Price = price;
            Volume = volume;
        }

        [StringLength(3)]
        [RegularExpression(@"^[A-Z]$",
         ErrorMessage = "Characters are not allowed.")]
        public string Symbol
        {
            get; set;
        }

        [Range(0.0, Double.MaxValue)]
        public double Price
        {
            get;
            set;
        }

        [Range(0.0, Double.MaxValue)]
        public double Volume
        {
            get;
            set;
        }

        public void UpdateQuote(double price, double volume)
        {
            Price = price;
            Volume = volume;
        }
    }
}
