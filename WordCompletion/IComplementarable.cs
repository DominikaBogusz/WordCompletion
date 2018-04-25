using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public interface IComplementarable
    {
        void Insert(string word, int usesCount = 1);
        void InsertWords(Dictionary<string, int> dictionary);
        void ResetWords(Dictionary<string, int> dictionary);
        Dictionary<string, int> GetAllWords();
        Dictionary<string, int> FindMatches(string prefix, bool sortByUsesCount, int max = 0);
        void Clear();
    }
}
