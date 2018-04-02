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
        Dictionary<string, int> FindAllMatches(string prefix);
        Dictionary<string, int> FindMostUsedMatchesDictionary(string prefix, int max = 0);
        List<string> FindMostUsedMatchesList(string prefix, int max = 0);
        void Clear();
    }
}
