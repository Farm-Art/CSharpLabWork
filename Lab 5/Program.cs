using System;
using System.Collections.Generic;

namespace Lab_5
{
    public static class Program
    {
        private static Random _rand = new Random();
        public static int[] WorkingHours { get; }
        public static int[] Statistics { get; }
        public static int MaxWaitTime { get; set; }  // In minutes
        public static int Hour { get; private set; }
        public static int Minute { get; private set; }
        public static int Step { get; }
        private static List<Person> People { get; set; }
        public const int OpenHour = 8;
        public const int CloseHour = 20;
        private const int PerFloor = 30;

        static Program()
        {
            WorkingHours = new int[CloseHour - OpenHour];
            for (int i = OpenHour; i < CloseHour; i++)
                WorkingHours[i - OpenHour] = i;
            Statistics = new int[24];
            Hour = OpenHour;
            Step = 2;
        }

        public static void Main(string[] args)
        {
            int amount = GetFloorsAmount();
            var e1 = new Elevator(0, amount - 1, 0);
            var e2 = new Elevator(0, amount - 1, amount - 1);
            
            People = GeneratePeople(amount, new [] { e1, e2 });

            while (Hour < CloseHour)
            {
                e1.Move();
                e2.Move();
                Minute += Step;
                Hour += Minute / 60;
                Minute %= 60;
            }

            for (int i = OpenHour; i < CloseHour; i++)
                Console.WriteLine($"{i:00}:00-{i + 1:00}:00 : {Statistics[i]}");
            Console.WriteLine($"Max wait time: {MaxWaitTime / 60:00}:{MaxWaitTime % 60:00}");
        }

        public static List<Person> GeneratePeople(int amountOfFloors, Elevator[] elevators)
        {
            var hours = GaussianSpread(WorkingHours);
            var output = new List<Person>(amountOfFloors * PerFloor);
            for (int i = 0; i < output.Capacity; i++)
            {
                var p = new Person(GetSpawnHour(hours), _rand.Next(59),
                    _rand.Next(amountOfFloors - 1));
                p.Board += OnBoard;
                foreach (var e in elevators)
                    e.Arrival += p.OnArrival;
                output.Add(p);
            }

            return output;
        }

        public static void OnBoard(Person p, Elevator e)
        {
            People.Remove(p);
            p.Board -= OnBoard;
        }

        public static int GetFloorsAmount()
        {
            Console.WriteLine("Enter amount of floors:");
            int amount;
            while (!Int32.TryParse(Console.ReadLine(), out amount) ||
                   amount < 2)
                Console.WriteLine("The value entered is not a valid amount of floors, try again.");
            return amount;
        }

        public static int GetSpawnHour(int[] hours)
        {
            return hours[_rand.Next(hours.Length - 1)];
        }
        
        public static T[] GaussianSpread<T>(T[] items)
        {
            int amount = items.Length;
            int[] weights = new int[amount];
            int total = 0;
            int lim = amount / 2 + (amount % 2 == 0 ? 0 : 1);
            for (int i = 0; i < lim; i++)
            {
                weights[i] = weights[amount - 1 - i] = i + 1;
                total += (i + 1) * (i == amount - 1 - i ? 1 : 2);
            }
            
            T[] output = new T[total];
            int curr = 0;
            for (int i = 0; i < amount; i++)
                for (int j = 0; j < weights[i]; j++)
                    output[curr++] = items[i];
            return output;
        }
    }
}