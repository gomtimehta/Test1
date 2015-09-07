using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        /// <summary>
        /// Invoke get and set operations on QuoteCache to run parallelly and test that get always returns the latest value set for the quote. 
        /// The output is displayed on console as well as in separate files for each thread. Output shows the time when thread ran and quote being set or retrieved
        /// 
        /// Validation
        /// There are 2 Quote classes, One is Entity and other is Model. The model is using Data Validation attributes for Validations and is more useful for UI. 
        /// The Entity Quote provides a method on itself that returns true if The Quote is valid and false otherwise.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //There are 2 implementations of Cache, one using simple dictionary and another uisng concurrent dictionary, 
            //replace QuoteCache by QuoteCacheUsingConcurrentDictionary to run the program using concurrent dictionary implementation

            IQuoteCache quoteCache = new QuoteCache();
            string outputFodlerPath = $"{Environment.CurrentDirectory}{"\\OutputFiles"}";

            Parallel.Invoke
                (
                       () =>
                       {
                           var quote = quoteCache.Get("MSF");
                           if (quote != null)
                           {
                               string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Getting MSFT Price " +
                                                     quote.Price + " Volume " + quote.Volume;
                               File.AppendAllText(outputFodlerPath + "\\T1.txt", outputString);
                               Console.WriteLine(outputString);
                           }
                       
                       }, 
                       () =>
                       {
                           quoteCache.Set("MSF",100, 10);

                           string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Setting MSFT 100, 10";
                           Console.WriteLine(outputString);
                           File.AppendAllText(outputFodlerPath + "\\T7.txt", outputString);
                       },
                       () =>
                       {
                           quoteCache.Set("MSFT", 200, 20); //This set operation should not execute
                            string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Setting MSFT 200, 20";
                           Console.WriteLine(outputString);
                           File.AppendAllText(outputFodlerPath + "\\T4.txt", outputString);
                       },
                      () =>
                      {
                          var quote = quoteCache.Get("MSF");

                          if (quote != null)
                          {
                              string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Getting MSFT Price " +
                                                                              quote.Price + " Volume " + quote.Volume;
                              File.AppendAllText(outputFodlerPath + "\\T2.txt", outputString);
                              Console.WriteLine(outputString);
                          }
                      },
                      () =>
                      {
                          quoteCache.Set("MSF", 300, 20);
                          string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Setting MSFT 300, 20";
                          Console.WriteLine(outputString);
                          File.AppendAllText(outputFodlerPath + "\\T5.txt", outputString);
                         
                      },
                      () =>
                      {
                          quoteCache.Set("MSF", 400, 20);
                          string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Setting MSFT 400, 20";
                          Console.WriteLine(outputString);
                          File.AppendAllText(outputFodlerPath + "\\T6.txt", outputString);
                      },
                      () =>
                      {
                          var quote = quoteCache.Get("MSF");
                          if (quote != null)
                          {
                              string outputString = "Time of operation " + DateTime.Now.ToString("HH:mm:ss.ffffff") + " Getting MSFT Price " +
                                                     quote.Price + " Volume " + quote.Volume;
                              File.AppendAllText(outputFodlerPath + "\\T3.txt", outputString);
                              Console.WriteLine(outputString);
                          }
                      }
                );

            Console.Read();

        }

    }
}
