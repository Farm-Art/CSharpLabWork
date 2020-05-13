using System;

namespace Experiments
{
    public class Car : Vehicle
    {
        public int Seats { get; }

        public Car(int seats)
        {
            Seats = seats;
        }

        public override void Print()
        {
            Console.WriteLine(" ______");
            Console.WriteLine("/__|__|\\__");
            Console.WriteLine("|________/");
            Console.WriteLine("  o    o");
            base.Print();
        }

        public override void Move()
        {
            Console.WriteLine("Я легковушка, врум-врум, еду.");
        }

        public override string ToString()
        {
            return $"Легковая машина, мест: {Seats}.";
        }
    }
}