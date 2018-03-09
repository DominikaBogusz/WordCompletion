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
                for (int i = 0; i < word.Value; i++)
                {
                    trie.Insert(word.Key);
                }
            }
        }

        public void Insert(string word)
        {
            trie.Insert(word);
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
