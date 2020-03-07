using System;
using System.Collections;

namespace Python
{
    public static class Stuff
    {
        public static string PrintSep { get; set; }
        public static string PrintEnd { get; set; }

        static Stuff()
        {
            PrintEnd = "\n";
            PrintSep = " ";
        }
        
        public static void Print(params object[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                Console.Write(strs[i] + (i < strs.Length - 1 ? PrintSep : ""));
            }

            Console.Write(PrintEnd);
        }
        
        public static Range Range(int start, int stop, int step) => new Range(start, stop, step);
        public static Range Range(int start, int stop) => new Range(start, stop);
        public static Range Range(int stop) => new Range(stop);
    }
    
    public class Range : IEnumerable
    {
        private int _start = 0, _stop, _step = 1;
        public Range(int start, int stop, int step)
        {
            _start = start;
            _stop = stop;
            _step = step;
        }

        public Range(int start, int stop)
        {
            _start = start;
            _stop = stop;
            if (stop < start)
                _step = -1;
        }

        public Range(int stop)
        {
            _stop = stop;
            if (stop < 0)
                _step = -1;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = _start; (_step > 0 ? i < _stop : i > _stop); i += _step)
                yield return i;
        }
    }
}