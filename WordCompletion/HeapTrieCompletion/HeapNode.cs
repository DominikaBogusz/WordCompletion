using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public class HeapNode : IComparable<HeapNode>
    {
        public string Word { get; set; }
        public int SearchCount { get; set; }

        public HeapNode(string word, int searchCount)
        {
            Word = word;
            SearchCount = searchCount;
        }

        public int CompareTo(HeapNode other)
        {
            if (this.SearchCount > other.SearchCount) return -1;
            if (this.SearchCount == other.SearchCount)
            {
                int stringCompare = string.Compare(this.Word, other.Word);
                if (stringCompare < 0) return -1;
                if (stringCompare == 0) return 0;
                if (stringCompare > 0) return 1;
            }
            return 1;
        }
    }
}
