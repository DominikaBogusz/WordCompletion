using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class SimpleCompletion : IComplementarable
    {
        public Dictionary<string, int> WordsDictionary { get; private set; }
        
        public SimpleCompletion()
        {
            WordsDictionary = new Dictionary<string, int>();
        }

        public SimpleCompletion(Dictionary<string, int> initialDictionary)
        {
            WordsDictionary = initialDictionary;
        }

        public void Insert(string word)
        {
            if (WordsDictionary.ContainsKey(word))
            {
                WordsDictionary[word] += 1;
            }
            else
            {
                WordsDictionary.Add(word, 1);
            }
        }

        public Dictionary<string, int> FindMatches(string prefix)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            foreach(var word in WordsDictionary)
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
    }
}
