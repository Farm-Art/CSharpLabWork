using System;
using Python;
using static Python.Stuff;
namespace Experiments
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            PrintSep = "; ";
            PrintEnd = "\n\n";
            Print(10, 20, "ahaha");
            
        }
    }   
}