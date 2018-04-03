using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class TrieCompletion : Completion, IComplementarable
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

        public Dictionary<string, int> FindAllMatches(string prefix)
        {
            return trie.FindMatches(prefix);
        }

        public Dictionary<string, int> FindMostUsedMatchesDictionary(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);
            return GetOrderedMatchesDictionary(matches, max);
        }

        public List<string> FindMostUsedMatchesList(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);
            return GetOrderedMatchesList(matches, max);
        }

        public void Clear()
        {
            trie = new Trie();
        }
    }
}
