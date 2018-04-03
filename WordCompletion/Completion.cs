using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public abstract class Completion
    {
        public Dictionary<string, int> GetOrderedMatchesDictionary(Dictionary<string, int> matches, int max = 0)
        {
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

        public List<string> GetOrderedMatchesList(Dictionary<string, int> matches, int max = 0)
        {
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
    }
}
