using System;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Castle castleA = new Castle(4);
            Castle castleB = new Castle(2);
            while (!castleA.IsDefeated && !castleB.IsDefeated)
            {
                Console.WriteLine("Castle A:");
                castleA.PrintResidents();
                Console.WriteLine(new String('-', 30));
                Console.WriteLine("Castle B:");
                castleB.PrintResidents();
                Console.WriteLine(new String('=', 30));
                castleA.WageWar(castleB);
                castleB.WageWar(castleA);
            }
        }
    }
}