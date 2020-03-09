using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_1
{
    public class List<T> : IEnumerable<T>
    {
        private ListItem
            _root = null,  // First item
            _tail = null,  // Last item
            _lastItem = null;  // Last indexed item
        private int _lastIndex = 0;  // Last requested index
        
        public int Length { get; private set; }
        public bool IsEmpty => Length == 0;

        ~List()
        {
            while (_root++ != null)
                _root.Remove();
        }

        // Push new value to the end of the list
        public void Add(T value)
        {
            ListItem item = new ListItem(value);
            if (IsEmpty)
                _root = _lastItem = item;
            else
                _tail.InsertAfter(item);
            _tail = item;
            Length++;
        }
        
        public void Remove() => Remove(Length - 1);

        public void Remove(int index)
        {
            GetItem(index).Remove();
            if (index == Length - 1)
            {
                // If popped from back, update index and lastItem to point to new tail
                _lastItem = _tail = _tail.Prev;
                _lastIndex--;
            }
            else
            {
                // Preserve index and update item ptr to match
                _lastItem = _lastItem.Next;
                if (index == 0)
                    // _root = _root.Next;
                    _root++;
            }
            Length--;
        }

        public void Sort(Comparison<T> cond, bool reverse = false)
        {
            for (int i = 0; i < Length - 1; i++)
            {
                // Check for swaps, break if the list is sorted.
                bool swapped = false;
                for (int j = 0; j < Length - 1 - i; j++)
                {
                    ListItem item = GetItem(j);
                    // Compare values and store result in score variable
                    int score = cond(item.Value, item.Next.Value);
                    // Invert if required
                    score *= (reverse ? -1 : 1);
                    
                    // Skip if in order
                    if (score <= 0) continue;
                    // Swap otherwise
                    ListItem.SwapValues(item, item.Next);
                    swapped = true;
                }

                if (!swapped)
                    break;
            }
        }

        // Apply a callback to each item
        public void Iter(Action<T> func)
        {
            foreach (T item in this)
                func(item);
        }

        public void Iter(Func<T, T> func)
        {
            for (int i = 0; i < Length; i++)
                this[i] = func(this[i]);
        }

        public T this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }

        private ListItem GetItem(int index)
        {
            if (!IsValidIndex(index))
            {
                throw new ArgumentOutOfRangeException();
            }
            
            /*
             * In order to optimize indexing speeds, the list stores
             * not one, but three starting points: the root, the tail
             * and the last indexed item. When a new item is requested,
             * the program runs to it from the closest point, which
             * improves performance for sequential access significantly.
             */
            
            // Determine closest starting point
            int difference = Math.Abs(_lastIndex - index);
            bool forward;
            
            // If closest to last indexed item
            if (difference < index && difference < Length - 1 - index)
            {
                // Determine run direction
                forward = index > _lastIndex;
            }
            // If closest to root
            else if (index < Length / 2)
            {
                _lastItem = _root;
                _lastIndex = 0;
                forward = true;
            }
            // If closest to tail
            else
            {
                _lastItem = _tail;
                _lastIndex = Length - 1;
                forward = false;
            }

            // Run towards requested index
            while (_lastIndex != index)
            {
                // Update current index and item
                _lastItem = (forward ? _lastItem.Next : _lastItem.Prev);
                _lastIndex += (forward ? 1 : -1);
            }

            return _lastItem;
        }

        public bool IsValidIndex(int index)
        {
            return index > -1 && index < Length;
        }

        public IEnumerator<T> GetEnumerator()
        {
            // 'yield return' allows us to create a generator in-place,
            // without the need to create a separate Enumerator class manually
            for (var curr = _root; curr != null; curr++)
                yield return curr.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            // Return a string in the following format:
            // {item1; item2; ...; itemN-1}
            string output = "{";
            for (var curr = _root; curr != null; curr++)
                output += curr;
            return output + "}";
        }

        private class ListItem
        {
            public static void SwapValues(ListItem a, ListItem b)
            {
                T temp = b.Value;
                b.Value = a.Value;
                a.Value = temp;
            }
            public T Value { get; set; }
            public ListItem Next { get; private set; }
            public ListItem Prev { get; private set; }

            public ListItem(T value)
            {
                Value = value;
            }

            public void InsertAfter(ListItem item)
            {
                if (Next != null)
                    Next.Prev = item;
                item.Prev = this;
                item.Next = Next;
                Next = item;
            }

            public void InsertBefore(ListItem item)
            {
                if (Prev != null)
                    Prev.Next = item;
                item.Next = this;
                item.Prev = Prev;
                Prev = item;
            }

            public void Remove()
            {
                if (Next != null)
                    Next.Prev = Prev;
                if (Prev != null)
                    Prev.Next = Next;
            }
            
            public static ListItem operator++ (ListItem item)
            {
                return item.Next;
            }

            public static ListItem operator-- (ListItem item)
            {
                return item.Prev;
            }

            public override string ToString() => Value + (Next != null ? "; " : "");
        }
    }
}