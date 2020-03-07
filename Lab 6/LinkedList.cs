using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_6
{
    #nullable enable
    public class LinkedList : IList
    {
        private LinkedListItem _root = null, _tail = null, _lastItem = null;
        private int _lastIndex = 0;
        
        public int Count { get; private set; }
        public bool IsSynchronized => false;
        public object SyncRoot => this;
        public bool IsFixedSize { get; set; }
        public bool IsReadOnly { get; set; }
        
        public IEnumerator GetEnumerator()
        {
            for (var curr = _root; curr != null; curr++)
                yield return curr.Value;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Rank != 1 || Count + index > array.Length)
                throw new ArgumentException();
            foreach (var val in this)
                array.SetValue(val, index++);
        }
        
        public int Add(object? value)
        {
            if (IsReadOnly || IsFixedSize)
                throw new NotSupportedException();
            if (Count == 0)
            {
                _root = _tail = _lastItem = new LinkedListItem(value);
                _lastIndex = 0;
            } 
            else
            {
                _tail.InsertAfter(new LinkedListItem(value));
                _tail++;
            }   
            return Count++;
        }

        public void Clear()
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            var curr = _root;
            while (curr != null)
            {
                if (IsFixedSize)
                    curr.Value = default;
                else
                {
                    curr.Remove();
                    Count--;
                }   
                curr++;
            }
            if (!IsFixedSize)
            {
                _root = _tail = _lastItem = null;
                _lastIndex = 0;
            }
        }

        public bool Contains(object? value) => IndexOf(value) != -1;

            public int IndexOf(object? value)
        {
            var c = EqualityComparer<object?>.Default;
            for (int i=0; i < Count; i++)
                if (c.Equals(this[i], value))
                    return i;
            return -1;
        }

        public void Insert(int index, object? value)
        {
            if (IsReadOnly || IsFixedSize)
                throw new NotSupportedException();
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            GetItem(index).InsertBefore(new LinkedListItem(value));
            if (index == 0)
                _root--;
            Count++;
        }

        private LinkedListItem GetItem(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            int difference = Math.Abs(index - _lastIndex);
            bool forward;
            if (difference > index)
            {
                _lastItem = _root;
                _lastIndex = 0;
                forward = true;
            } 
            else if (difference > Count - 1 - index)
            {
                _lastItem = _tail;
                _lastIndex = Count - 1;
                forward = false;
            }
            else
            {
                forward = (index > _lastIndex);
            }

            while (_lastIndex != index)
            {
                _lastItem = (forward ? _lastItem.Next : _lastItem.Prev);
                _lastIndex += (forward ? 1 : -1);
            }

            return _lastItem;
        }

        public void Remove(object? value)
        {
            if (IsReadOnly || IsFixedSize)
                throw new NotSupportedException();
            int index = IndexOf(value);
            if (index != -1)
                RemoveAt(IndexOf(value));
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly || IsFixedSize)
                throw new NotSupportedException();
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            GetItem(index).Remove();
            if (index == 0)
            {
                _root++;
                return;
            }   
            if (index == Count - 1)
            {
                _lastItem--;
                _lastIndex--;
                _tail--;
            }
            else
            {
                _lastItem++;
            }

            Count--;
        }

        public override string ToString()
        {
            string output = "{";
            for (var curr = _root; curr != null; curr++)
                output += curr;
            return output + "}";
        }

        public object? this[int index]
        {
            get => GetItem(index).Value;
            set
            {
                if (IsReadOnly)
                    throw new NotSupportedException();
                GetItem(index).Value = value;
            }
        }
        
        private class LinkedListItem
        {
            public object? Value { get; set; }
            public LinkedListItem Next { get; private set; }
            public LinkedListItem Prev { get; private set; }

            public static void SwapValues(LinkedListItem lhs, LinkedListItem rhs)
            {
                object? temp = rhs.Value;
                rhs.Value = lhs.Value;
                lhs.Value = temp;
            }
            
            public LinkedListItem(object? value)
            {
                Next = null;
                Prev = null;
                Value = value;
            }

            public void InsertBefore(LinkedListItem item)
            {
                if (Prev != null)
                    Prev.Next = item;
                item.Prev = Prev;
                item.Next = this;
                Prev = item;
            }

            public void InsertAfter(LinkedListItem item)
            {
                if (Next != null)
                    Next.Prev = item;
                item.Next = Next;
                item.Prev = this;
                Next = item;
            }

            public void Remove()
            {
                if (Prev != null)
                    Prev.Next = Next;
                if (Next != null)
                    Next.Prev = Prev;
            }

            public override string ToString()
            {
                return Value + (Next != null ? "; " : "");
            }

            public static LinkedListItem operator ++(LinkedListItem item)
            {
                return item.Next;
            }

            public static LinkedListItem operator --(LinkedListItem item)
            {
                return item.Prev;
            }
        }
    }
    #nullable disable
}