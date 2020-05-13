using System;

namespace Experiments
{
    public class Truck : Vehicle
    {
        public int Payload { get; }

        public Truck(int payload)
        {
            Payload = payload;
        }

        public override void Move()
        {
            Console.WriteLine("Я грузовик, еду, медленно и громко");
        }

        public override void Print()
        {
            Console.WriteLine("______");
            Console.WriteLine("|    |-\\");
            Console.WriteLine("______==");
            Console.WriteLine("o    o");
            base.Print();
        }

        public override string ToString()
        {
            return $"Грузовик, грузоподъёмность: {Payload}.";
        }
    }
}