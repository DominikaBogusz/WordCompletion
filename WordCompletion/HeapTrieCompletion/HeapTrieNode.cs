using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCompletion
{
    public class HeapTrieNode
    {
        private char character;
        public LinkedList<HeapTrieNode> Children { get; set; }
        public HeapTrieNode Parent { get; set; }
        public bool IsEnd { get; set; }
        public int SearchCount { get; set; }

        public HeapTrieNode(char x)
        {
            character = x;
            Children = new LinkedList<HeapTrieNode>();
            IsEnd = false;
            SearchCount = 0;
        }

        public HeapTrieNode GetChild(char x)
        {
            if (Children.Any())
            {
                foreach (HeapTrieNode child in Children)
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
                    HeapTrieNode currentChild = Children.ElementAt(i);
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

        public Heap GetWordsHeap()
        {
            Heap heap = new Heap();
            if (IsEnd)
            {
                heap.Add(new HeapNode(ToWord(), SearchCount));
            }
            if (Children.Any())
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    HeapTrieNode currentChild = Children.ElementAt(i);
                    if (currentChild != null)
                    {
                        var childHeap = currentChild.GetWordsHeap();
                        for (int j = 0; j < childHeap.Size; j++)
                        {
                            heap.Add(childHeap.Nodes.ElementAt(j));
                        }
                    }
                }
            }
            return heap;
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
