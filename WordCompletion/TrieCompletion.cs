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

        public Dictionary<string, int> FindAllMatches(string prefix)
        {
            return trie.FindMatches(prefix);
        }

        public Dictionary<string, int> FindMostUsedMatchesDictionary(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            if (matches.Any())
            {
                var orderedMatches = matches.OrderByDescending(key => key.Value);

                if (max == 0)
                {
                    foreach (var pair in orderedMatches)
                    {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                }
                else
                {
                    int i = 0;
                    while (i < max && i < orderedMatches.Count())
                    {
                        dictionary.Add(orderedMatches.ElementAt(i).Key, orderedMatches.ElementAt(i).Value);
                        i++;
                    }
                }
            }
            return dictionary;
        }

        public List<string> FindMostUsedMatchesList(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);
            List<string> list = new List<string>();
            if (matches.Any())
            {
                var orderedMatches = matches.OrderByDescending(key => key.Value);

                if (max == 0)
                {
                    foreach (var pair in orderedMatches)
                    {
                        list.Add(pair.Key);
                    }
                }
                else
                {
                    int i = 0;
                    while (i < max && i < orderedMatches.Count())
                    {
                        list.Add(orderedMatches.ElementAt(i).Key);
                        i++;
                    }
                }
            }
            return list;
        }

        public void Clear()
        {
            trie = new Trie();
        }
    }
}
