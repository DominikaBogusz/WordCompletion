using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class SimpleCompletion : IComplementarable
    {
        private Dictionary<string, int> wordsDictionary = new Dictionary<string, int>();

        public void Insert(string word, int usesCount = 1)
        {
            if (wordsDictionary.ContainsKey(word))
            {
                wordsDictionary[word] += usesCount;
            }
            else
            {
                wordsDictionary.Add(word, usesCount);
            }
        }

        public void InsertWordsDictionary(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                Insert(word.Key, word.Value);
            }
        }

        public void ResetWordsDictionary(Dictionary<string, int> dictionary)
        {
            wordsDictionary = dictionary;
        }

        public Dictionary<string, int> GetAllWords()
        {
            return wordsDictionary;
        }

        public Dictionary<string, int> FindMatches(string prefix)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            foreach(var word in wordsDictionary)
            {
                if (word.Key.StartsWith(prefix))
                {
                    matches.Add(word.Key, word.Value);
                }
            }
            return matches;
        }

        public List<string> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = FindMatches(prefix);
            List<string> list = new List<string>();
            if (matches.Any())
            {
                foreach (var item in matches.OrderByDescending(key => key.Value))
                {
                    list.Add(item.Key);
                }
            }
            if (max > 0 && list.Count > max)
            {
                return list.Take(max).ToList();
            }
            return list;
        }

        public void Clear()
        {
            wordsDictionary = new Dictionary<string, int>();
        }
    }
}
