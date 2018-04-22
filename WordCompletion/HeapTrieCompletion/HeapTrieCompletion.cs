using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class HeapTrieCompletion : IComplementarable
    {
        private HeapTrie userWords = new HeapTrie();
        private HeapTrie vocabularyWords = new HeapTrie();
        private bool sortingByUsesCount = true;
        private bool usingPLVocabulary = false;

        public void Insert(string word, int usesCount = 1)
        {
            userWords.Insert(word, usesCount);
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                userWords.Insert(word.Key, word.Value);
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            userWords = new HeapTrie();
            InsertWords(dictionary);
        }

        public void SortByUsesCount(bool enable)
        {
            sortingByUsesCount = enable;
        }

        public void UsePLVocabulary(bool enable)
        {
            if (enable)
            {
                foreach (var word in new VocabularyFromTxt().GetVocabulary())
                {
                    vocabularyWords.Insert(word.Key, word.Value);
                }
            }
            else
            {
                vocabularyWords = new HeapTrie();
            }

            usingPLVocabulary = enable;
        }

        public Dictionary<string, int> GetAllUserWords()
        {
            return userWords.FindMatches("");
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
            Dictionary<string, int> matches = userWords.FindMatches(prefix);
            if (max > 0)
            {
                if (matches.Count > max)
                {
                    return matches.Take(max) as Dictionary<string, int>;
                }
                if (usingPLVocabulary)
                {
                    int i = 0;
                    Dictionary<string, int> vocabularyMatches = vocabularyWords.FindMatches(prefix);
                    while (matches.Count < max && i < vocabularyMatches.Count)
                    {
                        var element = vocabularyMatches.ElementAt(i);
                        matches.Add(element.Key, element.Value);
                        i++;
                    }
                }
                return matches;
            }
            else
            {
                if (usingPLVocabulary)
                {
                    foreach (var element in vocabularyWords.FindMatches(prefix))
                    {
                        matches.Add(element.Key, element.Value);
                    }
                }
                return matches;
            }
        }

        public Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = new Dictionary<string, int>();
            Heap heap = userWords.FindMatchesHeap(prefix);
            int iterMax = heap.Size > max && max > 0 ? max : heap.Size;
            for (int i = 0; i < iterMax; i++)
            {
                var y = heap.PopMin();
                matches.Add(y.Word, y.SearchCount);
            }
            return matches;
        }

        public void Clear()
        {
            userWords = new HeapTrie();
        }
    }
}
