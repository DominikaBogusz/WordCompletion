﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public interface IComplementarable
    {
        void Insert(string word);
        Dictionary<string, int> FindMatches(string prefix);
        List<string> FindMostUsedMatches(string prefix, int max = 0);
    }
}
