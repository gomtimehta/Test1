using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class QuoteCacheTests
    {
        [Fact]
        public void SetShouldSEtTheQuoteValueCorrectlyForASymbol()
        {
            IQuoteCache cache = new QuoteCache();
            cache.Set("MSF", 10, 20);

            var quote = cache.Get("MSF");

            quote.Should().NotBeNull();
            quote.Symbol.Should().Be("MSF");
            quote.Price.Should().Be(10);
            quote.Volume.Should().Be(20);
        }
        [Fact]
        public void CacheShouldAlwaysStoreLatestValueSetForSymbol()
        {
            IQuoteCache cache = new QuoteCache();
            cache.Set("MSF", 10,20);
            cache.Set("MSF", 20,10);

            var quote = cache.Get("MSF");
            quote.Price.Should().Be(20);
            quote.Volume.Should().Be(10);
        }

        [Fact]
        public void QuoteShouldNotBeSetIfTheSymbolIsInvalid()
        {
            IQuoteCache cache = new QuoteCache();
            cache.Set("Test", 10, 20);

            var quote = cache.Get("Test");
            quote.Should().Be(null);
        }

        [Fact]
        public void QuoteShouldNotBeSetIfThePriceIsInvalid()
        {
            IQuoteCache cache = new QuoteCache();
            cache.Set("MSF", -10, 20);

            var quote = cache.Get("MSF");
            quote.Should().Be(null);
        }

        [Fact]
        public void QuoteShouldNotBeSetIfTheVolumeIsInvalid()
        {
            IQuoteCache cache = new QuoteCache();
            cache.Set("MSF", 10, -20);

            var quote = cache.Get("MSF");
            quote.Should().Be(null);
        }
    }
}
