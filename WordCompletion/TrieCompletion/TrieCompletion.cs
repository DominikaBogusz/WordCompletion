using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class TrieCompletion : IComplementarable
    {
        private Trie words = new Trie();

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
            words = new Trie();
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

        public Dictionary<string, int> FindUnorderedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = words.FindMatches(prefix);
            if (max > 0 && matches.Count > max)
            {
                return matches.Take(max) as Dictionary<string, int>;
            }
            return matches;
        }

        public Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> output = new Dictionary<string, int>();
            Dictionary<string, int> matches = words.FindMatches(prefix);
            if (matches.Any())
            {
                var orderedMatches = matches.OrderByDescending(key => key.Value);
                int iterMax = orderedMatches.Count() > max && max > 0 ? max : orderedMatches.Count();
                for (int i = 0; i < iterMax; i++)
                {
                    var element = orderedMatches.ElementAt(i);
                    output.Add(element.Key, element.Value);
                }
            }
            return output;
        }

        public void Clear()
        {
            words = new Trie();
        }
    }
}
