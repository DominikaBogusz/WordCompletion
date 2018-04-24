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

        public CompletionManager(IComplementarable completionSource, Dictionary<string, int> initialWords = null, bool sortByUsesCount = true, bool useVocabulary = false)
        {
            completion = completionSource;
            if (initialWords != null)
            {
                completion.InsertWords(initialWords);
            }
            completion.SortByUsesCount(sortByUsesCount);
            completion.UsePLVocabulary(useVocabulary);
        }

        public void SortByUsesCount(bool enable)
        {
            completion.SortByUsesCount(enable);
        }

        public void UsePLVocabulary(bool enable)
        {
            completion.UsePLVocabulary(enable);
        }

        public void InsertWord(string word)
        {
            completion.Insert(word);
        }

        public Dictionary<string, int> GetMatches(string prefix, int max = 0)
        {
            return completion.FindMatches(prefix, max);
        }

        public Dictionary<string, int> GetAllUserWords()
        {
            return completion.GetAllUserWords();
        }

        public void ResetUserWords(Dictionary<string, int> words)
        {
            completion.ResetWords(words);
        }
    }
}
