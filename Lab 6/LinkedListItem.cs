namespace Lab_6
{
    public partial class LinkedList
    {
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
}