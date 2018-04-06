using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public class HeapTrie
    {
        private HeapTrieNode root;

        public HeapTrie()
        {
            root = new HeapTrieNode(' ');
        }

        public void Insert(string word, int usesCount = 1)
        {
            if (!Search(word))
            {
                HeapTrieNode current = root;
                HeapTrieNode previous;
                foreach (char x in word.ToCharArray())
                {
                    previous = current;
                    HeapTrieNode child = current.GetChild(x);
                    if (child != null)
                    {
                        current = child;
                        child.Parent = previous;
                    }
                    else
                    {
                        current.Children.AddLast(new HeapTrieNode(x));
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
            HeapTrieNode current = root;
            foreach (char x in word.ToCharArray())
            {
                HeapTrieNode child = current.GetChild(x);
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
                current.SearchCount += 1;
                return true;
            }
            return false;
        }

        public Heap FindMatchesHeap(string prefix)
        {
            HeapTrieNode lastNode = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                lastNode = lastNode.GetChild(prefix[i]);
                if (lastNode == null)
                {
                    return new Heap();
                }
            }
            return lastNode.GetWordsHeap();
        }

        public Dictionary<string, int> FindMatches(string prefix)
        {
            HeapTrieNode lastNode = root;
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
