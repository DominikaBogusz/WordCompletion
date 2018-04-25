using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class SimpleCompletion : IComplementarable
    {
        private Dictionary<string, int> words = new Dictionary<string, int>();

        public void Insert(string word, int usesCount = 1)
        {
            if (words.ContainsKey(word))
            {
                words[word] += usesCount;
            }
            else
            {
                words.Add(word, usesCount);
            }
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                Insert(word.Key, word.Value);
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            words.Clear();
            words = dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        public Dictionary<string, int> GetAllWords()
        {
            return words;
        }

        public Dictionary<string, int> FindMatches(string prefix, bool sortByUsesCount, int max = 0)
        {
            return sortByUsesCount ? FindMostUsedMatches(prefix, max) : FindUnorderedMatches(prefix, max);
        }

        private Dictionary<string, int> FindUnorderedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (word.Key.StartsWith(prefix))
                {
                    matches.Add(word.Key, word.Value);
                }
            }
            if(max > 0 && matches.Count > max)
            {
                return matches.Take(max) as Dictionary<string, int>;
            }
            return matches;
        }

        private Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> output = new Dictionary<string, int>();
            Dictionary<string, int> matches = FindUnorderedMatches(prefix);
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
            words.Clear();
        }
    }
}
