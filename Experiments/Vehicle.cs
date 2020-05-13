using System;

namespace Experiments
{
    public abstract class Vehicle
    {
        public virtual void Move()
        {
            Console.WriteLine("Я еду, врум-врум");
        }

        public virtual void Print()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return "I'm a vehicle";
        }
    }
}