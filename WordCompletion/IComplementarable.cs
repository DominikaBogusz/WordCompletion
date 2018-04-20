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
        void UsePLVocabulary(bool enable);
        Dictionary<string, int> GetAllWords();
        Dictionary<string, int> FindMatches(string prefix, int max = 0);
        Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0);
        void Clear();
    }
}
