using System;
using System.Collections.Generic;
using System.Text;

namespace WordCompletion
{
    public class Heap
    {
        public List<HeapNode> Nodes { get; private set; }

        public int Size
        {
            get
            {
                return Nodes.Count;
            }
        }

        public Heap()
        {
            Nodes = new List<HeapNode>();
        }

        public HeapNode GetMin()
        {
            return Nodes.Count > 0 ? Nodes[0] : default(HeapNode);
        }

        public void Add(HeapNode item)
        {
            Nodes.Add(item);
            HeapifyUp(Nodes.Count - 1);
        }

        public void IncreaseSearchCount(HeapNode item, int increaseValue)
        {
            item.SearchCount += increaseValue;
            HeapifyDown(0);
        }

        public HeapNode PopMin()
        {
            if (Nodes.Count > 0)
            {
                HeapNode item = Nodes[0];
                Nodes[0] = Nodes[Nodes.Count - 1];
                Nodes.RemoveAt(Nodes.Count - 1);

                HeapifyDown(0);
                return item;
            }

            throw new InvalidOperationException("No elements in heap!");
        }

        private void HeapifyUp(int index)
        {
            var parent = GetParent(index);
            if (parent >= 0 && Nodes[index].CompareTo(Nodes[parent]) < 0)
            {
                var temp = Nodes[index];
                Nodes[index] = Nodes[parent];
                Nodes[parent] = temp;

                HeapifyUp(parent);
            }
        }

        private void HeapifyDown(int index)
        {
            var smallest = index;

            var left = GetLeft(index);
            var right = GetRight(index);

            if (left < Size && Nodes[left].CompareTo(Nodes[index]) < 0)
            {
                smallest = left;
            }

            if (right < Size && Nodes[right].CompareTo(Nodes[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var temp = Nodes[index];
                Nodes[index] = Nodes[smallest];
                Nodes[smallest] = temp;

                HeapifyDown(smallest);
            }

        }

        private int GetParent(int index)
        {
            if (index <= 0)
            {
                return -1;
            }

            return (index - 1) / 2;
        }

        private int GetLeft(int index)
        {
            return 2 * index + 1;
        }

        private int GetRight(int index)
        {
            return 2 * index + 2;
        }
    }
}
