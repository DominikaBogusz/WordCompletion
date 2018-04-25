using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class HeapTrieCompletion : IComplementarable
    {
        private HeapTrie words = new HeapTrie();

        public void Insert(string word, int usesCount = 1)
        {
            words.Insert(word, usesCount);
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                words.Insert(word.Key, word.Value);
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            words = new HeapTrie();
            InsertWords(dictionary);
        }

        public Dictionary<string, int> GetAllWords()
        {
            return words.FindMatches("");
        }

        public Dictionary<string, int> FindMatches(string prefix, bool sortByUsesCount, int max = 0)
        {
            return sortByUsesCount ? FindMostUsedMatches(prefix, max) : FindUnorderedMatches(prefix, max);
        }

        private Dictionary<string, int> FindUnorderedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = words.FindMatches(prefix);
            if (max > 0 && matches.Count > max)
            {
                return matches.Take(max) as Dictionary<string, int>;
            }
            return matches;
        }

        private Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            Heap heap = words.FindMatchesHeap(prefix);
            int iterMax = heap.Size > max && max > 0 ? max : heap.Size;
            for (int i = 0; i < iterMax; i++)
            {
                var y = heap.PopMin();
                matches.Add(y.Word, y.SearchCount);
            }
            return matches;
        }

        public void Clear()
        {
            words = new HeapTrie();
        }
    }
}
