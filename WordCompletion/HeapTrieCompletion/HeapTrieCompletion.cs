using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class HeapTrieCompletion : IComplementarable
    {
        private HeapTrie trie = new HeapTrie();

        public void Insert(string word, int usesCount = 1)
        {
            trie.Insert(word, usesCount);
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                trie.Insert(word.Key, word.Value);
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            trie = new HeapTrie();
            InsertWords(dictionary);
        }

        public void UsePLVocabulary(bool enable)
        {
            //TODO
        }

        public Dictionary<string, int> GetAllWords()
        {
            return trie.FindMatches("");
        }

        public Dictionary<string, int> FindMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);

            if (max > 0 && max < matches.Count)
            {
                return matches.Take(max) as Dictionary<string, int>;
            }
            else
            {
                return matches;
            }
        }

        public Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            Heap heap = trie.FindMatchesHeap(prefix);
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
            trie = new HeapTrie();
        }
    }
}
