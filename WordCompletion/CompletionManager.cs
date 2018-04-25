using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCompletion
{
    public class CompletionManager
    {
        private IComplementarable completion;

        private bool sortingByUsesCount;

        public CompletionManager(IComplementarable completionSource, Dictionary<string, int> initialWords = null, bool sortByUsesCount = true)
        {
            completion = completionSource;
            if (initialWords != null)
            {
                completion.InsertWords(initialWords);
            }

            sortingByUsesCount = sortByUsesCount;
        }

        public void SortByUsesCount(bool enable)
        {
            sortingByUsesCount = enable;
        }

        public void InsertWord(string word)
        {
            completion.Insert(word);
        }

        public Dictionary<string, int> GetMatches(string prefix, int max = 0)
        {
            return completion.FindMatches(prefix, sortingByUsesCount, max);
        }

        public Dictionary<string, int> GetAllWords()
        {
            return completion.GetAllWords();
        }

        public void ResetUserWords(Dictionary<string, int> words)
        {
            completion.ResetWords(words);
        }

        public void ClearWords()
        {
            completion.Clear();
        }
    }
}
