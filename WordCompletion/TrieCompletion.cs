using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class TrieCompletion : IComplementarable
    {
        private Trie trie = new Trie();

        public TrieCompletion()
        {
        }

        public TrieCompletion(Dictionary<string, int> initialDictionary)
        {
            foreach(var word in initialDictionary)
            {
                trie.Insert(word.Key, word.Value);
            }
        }

        public void Insert(string word, int usesCount = 1)
        {
            trie.Insert(word, usesCount);
        }

        public Dictionary<string, int> GetAllWords()
        {
            return trie.FindMatches("");
        }

        public Dictionary<string, int> FindMatches(string prefix)
        {
            return trie.FindMatches(prefix);
        }

        public List<string> FindMostUsedMatches(string prefix, int max = 0)
        {
            return trie.FindMostUsedMatches(prefix, max);
        }
    }
}
