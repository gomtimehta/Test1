﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IQuoteCache
    {
        void Set(string symbol, double price, double volume);
        Quote Get(string symbol);
    }

}
