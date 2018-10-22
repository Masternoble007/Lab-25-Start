﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.PriorityQueueLibrary
{
    public class MinPriorityQueue<TPriority, TValue> where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// A leftist heap storing the elements and their priorities.
        /// </summary>
        private LeftistTree<KeyValuePair<TPriority, TValue>> _elements = null;

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Adds the given element with the given priority.
        /// </summary>
        /// <param name="p">The priority of the element.</param>
        /// <param name="x">The element to add.</param>
        public void Add(TPriority p, TValue x)
        {
            LeftistTree<KeyValuePair<TPriority, TValue>> node =
                new LeftistTree<KeyValuePair<TPriority, TValue>>(new KeyValuePair<TPriority, TValue>(p, x), null, null);
            _elements = Merge(_elements, node);
            Count++;
        }

        /// <summary>
        /// Merges the given leftist heaps into one leftist heap.
        /// </summary>
        /// <param name="h1">One of the leftist heaps to merge.</param>
        /// <param name="h2">The other leftist heap to merge.</param>
        /// <returns>The resulting leftist heap.</returns>
        public static LeftistTree<KeyValuePair<TPriority, TValue>> Merge(LeftistTree<KeyValuePair<TPriority, TValue>> h1,
            LeftistTree<KeyValuePair<TPriority, TValue>> h2)
        {
            if (h1 == null)
            {
                return h2;
            } else if (h2 == null)
            {
                return h1;
            }
            else
            {
                LeftistTree<KeyValuePair<TPriority, TValue>> small = h1;
                LeftistTree<KeyValuePair<TPriority, TValue>> large = h2;
                if (small.Data.Key.CompareTo(large.Data.Key) > 0)
                {
                    small = h2;
                    large = h1;
                }
                return new LeftistTree<KeyValuePair<TPriority, TValue>>(small.Data, small.LeftChild, Merge(small.RightChild, large));
            }
        }

        /// <summary>
        /// To check for the minimum priority.
        /// </summary>
        public TPriority MinimumPriority
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException();
                }
                return _elements.Data.Key;
            }
        }

        /// <summary>
        /// Removes an element from MinimumPriority.
        /// </summary>
        /// <returns></returns>
        public TValue RemoveMinimumPriority()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            TValue v = _elements.Data.Value;
            _elements = Merge(_elements.LeftChild, _elements.RightChild);
            Count--;
            return v;
        }
    }
}
