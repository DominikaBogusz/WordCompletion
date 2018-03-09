using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCompletion
{
    class TrieNode
    {
        private char character;
        public LinkedList<TrieNode> Children { get; set; }
        public TrieNode Parent { get; set; }
        public bool IsEnd { get; set; }
        public int SearchCount { get; set; }

        public TrieNode(char x)
        {
            character = x;
            Children = new LinkedList<TrieNode>();
            IsEnd = false;
            SearchCount = 0;
        }

        public TrieNode GetChild(char x)
        {
            if (Children.Any())
            {
                foreach (TrieNode child in Children)
                {
                    if (child.character == x)
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        public Dictionary<string, int> GetWords()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            if (IsEnd)
            {
                dictionary.Add(ToWord(), SearchCount);
            }
            if (Children.Any())
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    TrieNode currentChild = Children.ElementAt(i);
                    if (currentChild != null)
                    {
                        foreach (var item in currentChild.GetWords())
                        {
                            dictionary.Add(item.Key, item.Value);
                        }
                    }
                }
            }
            return dictionary;
        }

        public string ToWord()
        {
            if (Parent != null)
            {
                return Parent.ToWord() + character;
            }
            return "";
        }
    }
}
