using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public interface IComplementarable
    {
        void Insert(string word, int usesCount = 1);
        void InsertWordsDictionary(Dictionary<string, int> dictionary);
        void ResetWordsDictionary(Dictionary<string, int> dictionary);
        Dictionary<string, int> GetAllWords();
        Dictionary<string, int> FindMatches(string prefix, int max = 0);
        Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0);
        void Clear();
    }
}
