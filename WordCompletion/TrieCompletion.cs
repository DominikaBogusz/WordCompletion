using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class TrieCompletion : IComplementarable
    {
        private Trie trie = new Trie();

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
            trie = new Trie();
            InsertWordsDictionary(dictionary);
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

        public void Clear()
        {
            trie = new Trie();
        }
    }
}
