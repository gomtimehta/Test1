using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// //This is a kind of utility that will be used by other parts of applications or may even be used by
    ///  different applications and it should throw exceptions so others users can handle it the way they want to. 
    /// </summary>
    public class QuoteCache : IQuoteCache
    {
        private readonly ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();

        private Dictionary<string, Quote> _quotes = new Dictionary<string, Quote>();


        public void Set(string symbol, double price, double volume)
        {
            if (String.IsNullOrEmpty(symbol))
            {
                return;
            }
            var quote = new Quote(symbol, price, volume);

            if (!quote.IsQuoteValid())
                return;

            _readerWriterLockSlim.EnterWriteLock();

            try
            {
                UpdateQuote(symbol, quote);
            }

            finally
            {
                _readerWriterLockSlim.ExitWriteLock();
            }

        }

        public Quote Get(string symbol)
        {
            Quote q = null;
            _readerWriterLockSlim.EnterReadLock();

            if (String.IsNullOrEmpty(symbol))
            {
                return null;
            }

            try
            {
                _quotes.TryGetValue(symbol, out q);
            }
            finally
            {
                _readerWriterLockSlim.ExitReadLock();
            }
            return q;
        }

        private void UpdateQuote(string symbol, Quote quote)
        {
            if (!_quotes.ContainsKey(symbol))
            {
                _quotes.Add(symbol, quote);
            }
            else
            {
                _quotes[symbol].UpdateQuote(quote.Price, quote.Volume);
            }
        }

    }
}
