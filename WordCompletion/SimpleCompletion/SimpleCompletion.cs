using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class SimpleCompletion : IComplementarable
    {
        private Dictionary<string, int> userWords = new Dictionary<string, int>();
        private Dictionary<string, int> vocabularyWords = new Dictionary<string, int>();
        private string vocabularyFile;
        private bool sortingByUsesCount = true;
        private bool usingPLVocabulary = false;

        public SimpleCompletion(string vocabularyFilePath)
        {
            vocabularyFile = vocabularyFilePath;
        }

        public void Insert(string word, int usesCount = 1)
        {
            if (userWords.ContainsKey(word))
            {
                userWords[word] += usesCount;
            }
            else
            {
                userWords.Add(word, usesCount);
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
            userWords.Clear();
            userWords = dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        public void SortByUsesCount(bool enable)
        {
            sortingByUsesCount = enable;
        }

        public void UsePLVocabulary(bool enable)
        {
            if (enable)
            {
                vocabularyWords = new VocabularyFromTxt(vocabularyFile).GetVocabulary();
            }
            else
            {
                vocabularyWords.Clear();
            }

            usingPLVocabulary = enable;
        }

        public Dictionary<string, int> GetAllUserWords()
        {
            return userWords;
        }

        public Dictionary<string, int> FindMatches(string prefix, int max = 0)
        {
            if (sortingByUsesCount)
            {
                return FindMostUsedMatches(prefix, max);
            }
            else
            {
                return FindUnorderedMatches(prefix, max);
            }
        }

        public Dictionary<string, int> FindUnorderedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            foreach (var word in userWords)
            {
                if (word.Key.StartsWith(prefix))
                {
                    matches.Add(word.Key, word.Value);
                }
            }
            if(max > 0)
            {
                if (matches.Count > max)
                {
                    return matches.Take(max) as Dictionary<string, int>;
                }
                if(usingPLVocabulary)
                {
                    int i = 0;
                    while (matches.Count < max && i < vocabularyWords.Count)
                    {
                        var element = vocabularyWords.ElementAt(i);
                        if (element.Key.StartsWith(prefix) && !matches.ContainsKey(element.Key))
                        {
                            matches.Add(element.Key, element.Value);
                        }
                        i++;
                    }  
                }
                return matches;
            }
            else
            {
                if (usingPLVocabulary)
                {
                    foreach (var element in vocabularyWords)
                    {
                        if (element.Key.StartsWith(prefix) && !matches.ContainsKey(element.Key))
                        {
                            matches.Add(element.Key, element.Value);
                        }
                    }
                }
                return matches;
            }
        }

        public Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
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
            userWords.Clear();
        }
    }
}
