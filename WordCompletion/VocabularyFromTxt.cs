using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordCompletion
{
    public class VocabularyFromTxt
    {
        string vocabularyFile;

        public VocabularyFromTxt(string vocabularyFilePath)
        {
            vocabularyFile = vocabularyFilePath;
        }

        public Dictionary<string, int> GetVocabulary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            if (File.Exists(vocabularyFile) == true)
            {
                foreach (string word in File.ReadLines(vocabularyFile))
                {
                    dictionary.Add(word, 1);
                }
            }
            return dictionary;
        }
    }
}
