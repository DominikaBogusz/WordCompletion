using System;
using System.Collections.Generic;
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

        public void InsertWordsDictionary(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                trie.Insert(word.Key, word.Value);
            }
        }

        public void ResetWordsDictionary(Dictionary<string, int> dictionary)
        {
            trie = new HeapTrie();
            InsertWordsDictionary(dictionary);
        }

        public Dictionary<string, int> GetAllWords()
        {
            return trie.FindMatches("");
        }

        public Dictionary<string, int> FindAllMatches(string prefix)
        {
            return trie.FindMatches(prefix);
        }

        public Dictionary<string, int> FindMostUsedMatchesDictionary(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();

            var x = trie.FindMatchesHeap(prefix);

            int iterMax = x.Size > max && max > 0 ? max : x.Size;

            for (int i = 0; i < iterMax; i++)
            {
                var y = x.PopMin();
                matches.Add(y.Word, y.SearchCount);
            }

            return matches;
        }

        public List<string> FindMostUsedMatchesList(string prefix, int max = 0)
        {
            List<string> matches = new List<string>();

            var x = trie.FindMatchesHeap(prefix);

            int iterMax = x.Size > max && max > 0 ? max : x.Size;

            for (int i = 0; i < iterMax; i++)
            {
                var y = x.PopMin();
                matches.Add(y.Word);
            }

            return matches;
        }

        public void Clear()
        {
            trie = new HeapTrie();
        }
    }
}
