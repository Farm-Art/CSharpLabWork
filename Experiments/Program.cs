using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace Experiments
{
    public static class Sample
    {
        public enum Confirmation
        {
            No = 0, N = 0, False = 0,
            Yes = 1, Y = 1, True = 1
        }
    }
    
    public static class Program
    {
        public static bool ProperParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {
            return Enum.TryParse(value, ignoreCase, out result) && Enum.IsDefined(typeof(TEnum), result);
        }

        public static bool ProperParse<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            return Enum.TryParse(value, out result) && Enum.IsDefined(typeof(TEnum), result);
        }
        
        public static void Main(string[] args)
        {
            while (true)
            {
                if (!ProperParse(Console.ReadLine(), out Sample.Confirmation val))
                    Console.WriteLine("WRONG");
                else
                    switch (val)
                    {
                        case Sample.Confirmation.Yes :
                            Console.WriteLine("yes");
                            break;
                        case Sample.Confirmation.False :
                            Console.WriteLine("no");
                            break;
                    }
            }
        }
    }
}