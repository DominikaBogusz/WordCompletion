using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCompletion
{
    public class Trie
    {
        private TrieNode root;

        public Trie()
        {
            root = new TrieNode(' ');
        }

        public void Insert(string word, int usesCount = 1)
        {
            if (!Search(word))
            {
                TrieNode current = root;
                TrieNode previous;
                foreach (char x in word.ToCharArray())
                {
                    previous = current;
                    TrieNode child = current.GetChild(x);
                    if (child != null)
                    {
                        current = child;
                        child.Parent = previous;
                    }
                    else
                    {
                        current.Children.AddLast(new TrieNode(x));
                        current = current.GetChild(x);
                        current.Parent = previous;
                    }
                }
                current.IsEnd = true;
                current.SearchCount += usesCount;
            }
        }

        public bool Search(string word)
        {
            TrieNode current = root;
            foreach (char x in word.ToCharArray())
            {
                TrieNode child = current.GetChild(x);
                if (child != null)
                {
                    current = current.GetChild(x);
                }
                else
                {
                    return false;
                }
            }
            if (current.IsEnd)
            {
                current.SearchCount++;
                return true;
            }
            return false;
        }

        public Dictionary<string, int> FindMatches(string prefix)
        {
            TrieNode lastNode = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                lastNode = lastNode.GetChild(prefix[i]);
                if (lastNode == null)
                {
                    return new Dictionary<string, int>();
                }
            }
            return lastNode.GetWords();
        }
    }
}
