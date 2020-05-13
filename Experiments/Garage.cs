using System;
using System.Collections.Generic;

namespace Experiments
{
    public class Garage
    {
        public List<Vehicle> Contents { get; } = new List<Vehicle>();

        public enum Command
        {
            Add = 1, Remove, Print, Quit
        }

        public enum CarType
        {
            Car, Truck
        }

        public Garage() {}

        public void Add()
        {
            Console.WriteLine("Do you want to add a car or a truck?");
            CarType value;
            while (!ProperParse(Console.ReadLine(), true, out value))
            {
                Console.WriteLine("That's not a valid car type!");
            }

            Vehicle item = null;
            switch (value)
            {
                case CarType.Car :
                    Console.WriteLine("Enter amount of seats");
                    
                    int seats = 0;
                    while (!Int32.TryParse(Console.ReadLine(), out seats)
                           || seats < 1)
                        Console.WriteLine("Failed to parse int or value is out of range, try again.");
                    
                    item = new Car(seats);
                    break;
                
                case CarType.Truck :
                    Console.WriteLine("Enter payload");
                    
                    int payload = 0;
                    while (!Int32.TryParse(Console.ReadLine(), out payload)
                           || payload < 1)
                        Console.WriteLine("Failed to parse int or value is out of range, try again.");
                    
                    item = new Truck(payload);
                    break;
            }
            Contents.Add(item);
        }

        public void Print()
        {
            foreach (var veh in Contents)
                veh.Print();
        }

        public void Remove()
        {
            Console.WriteLine("Enter index of vehicle to remove");
            int index = GetIndex();
            Contents[index].Move();
            Contents.RemoveAt(index);
        }

        public int GetIndex()
        {
            int value = -1;
            while (!Int32.TryParse(Console.ReadLine(), out value)
                   || value < 0 || value >= Contents.Count)
                Console.WriteLine("Index is not an int or is out of range.");
            return value;
        }

        public static bool ProperParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {
            // Regular Enum.TryParse doesn't check if the parsed integer has an associated constant;
            // This version merges parsing with definition checking.
            return Enum.TryParse(value, ignoreCase, out result) && Enum.IsDefined(typeof(TEnum), result);
        }

        public static bool ProperParse<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            return ProperParse(value, false, out result);
        }    
    }
}