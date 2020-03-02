using System;
using System.Collections.Generic;

namespace Lab_3
{
    public class Castle
    {
        private static Random _rand = new Random();
        private List<Character> _chars = null;
        
        // Return a living resident
        public Character Defender
        {
            get
            {
                for (int i = _chars.Count - 1; i >= 0; i--)
                    if (_chars[i].IsAlive)
                        return _chars[i];
                    else
                        _chars.RemoveAt(i);
                return null;
            }
        }

        // Check if there are any living residents
        public bool IsDefeated
        {
            get
            {
                foreach (var chr in _chars)
                    if (chr.IsAlive)
                        return false;
                return true;
            }
        }
        
        public Castle(int size)
        {
            if (size < 1)
                throw new ArgumentException("Every castle should have at least 1 resident");
            _chars = new List<Character>(size);
            // Every castle has a king
            _chars.Add(new King(_rand.Next(2, 4)));
            // The rest are knights
            for (int i = 1; i < size; i++)
                _chars.Add(new Knight(_rand.Next(0, 5)));
        }

        public void PrintResidents()
        {
            foreach (Character chr in _chars)
                if (chr.IsAlive)
                    Console.WriteLine(chr);
        }

        // Remove all dead residents
        public void CleanUp()
        {
            for (int i = _chars.Count-1; i >= 0; i--)
                if (!_chars[i].IsAlive)
                    _chars.RemoveAt(i);
        }
        
        public void WageWar(Castle other)
        {
            foreach (Character chr in _chars)
            {
                Character defender = other.Defender;
                if (defender != null)
                    chr.Fight(defender);
                else return;
            }
            other.CleanUp();
            CleanUp();
        }
    }
}