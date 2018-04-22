using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class HeapTrieCompletion : IComplementarable
    {
        private HeapTrie trie = new HeapTrie();
        Dictionary<string, int> wordsFromPLVocabulary = new Dictionary<string, int>();
        bool usingPLVocabulary;

        public HeapTrieCompletion(bool usePLVocabulary)
        {
            UsePLVocabulary(usePLVocabulary);
        }

        public void Insert(string word, int usesCount = 1)
        {
            trie.Insert(word, usesCount);
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            foreach (var word in dictionary)
            {
                trie.Insert(word.Key, word.Value);
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            trie = new HeapTrie();
            InsertWords(dictionary);
        }

        public void UsePLVocabulary(bool enable)
        {
            if (enable)
            {
                wordsFromPLVocabulary = new VocabularyFromTxt().GetVocabulary();
            }
            else
            {
                wordsFromPLVocabulary.Clear();
            }

            usingPLVocabulary = enable;
        }

        public Dictionary<string, int> GetAllWords()
        {
            return trie.FindMatches("");
        }

        public Dictionary<string, int> FindMatches(string prefix, int max = 0)
        {
            Dictionary<string, int> matches = trie.FindMatches(prefix);
            if (max > 0)
            {
                if (matches.Count > max)
                {
                    return matches.Take(max) as Dictionary<string, int>;
                }
                if (usingPLVocabulary)
                {
                    int i = 0;
                    while (matches.Count < max && i < wordsFromPLVocabulary.Count)
                    {
                        var element = wordsFromPLVocabulary.ElementAt(i);
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
                    foreach (var element in wordsFromPLVocabulary)
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
            Dictionary<string, int> matches = new Dictionary<string, int>();
            Heap heap = trie.FindMatchesHeap(prefix);
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
            trie = new HeapTrie();
        }
    }
}
