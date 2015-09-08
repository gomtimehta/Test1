using System;
using System.Collections.Concurrent;

namespace Core
{
    public class QuoteCacheUsingConcurrentDictionary : IQuoteCache
    {

        private ConcurrentDictionary<string, Quote> _quotes = new ConcurrentDictionary<string, Quote>();
         
        public void Set(string symbol, double price, double volume)
        {
            if (String.IsNullOrEmpty(symbol))
            {
                return;//Can throw invalid argument exception here
            }
            var quote = new Quote(symbol, price, volume);

            _quotes.AddOrUpdate(symbol, quote, (key, oldValue) => quote);
        }

        public Quote Get(string symbol)
        {
            Quote quote;

            if (String.IsNullOrEmpty(symbol))
            {
                return null;
            }

            _quotes.TryGetValue(symbol, out quote);

            return quote;
        }
    }
}
