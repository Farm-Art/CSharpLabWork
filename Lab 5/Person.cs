namespace Lab_5
{
    public class Person
    {
        public int SpawnHour { get; }
        public int SpawnMinute { get; }
        public int Destination { get; }
        public bool Traveling { get; private set; }

        public bool Active =>
            Program.Hour == SpawnHour ?
                SpawnMinute <= Program.Minute :
                SpawnHour < Program.Hour;

        public Person(int spawnHour, int spawnMinute, int destination)
        {
            SpawnHour = spawnHour;
            SpawnMinute = spawnMinute;
            Destination = destination;
        }

        public void OnArrival(Elevator e)
        {
            if (e.Floor == 0 && Active && !Traveling)
            {
                Traveling = true;
                e.Contents.Add(this);
                Board?.Invoke(this, e);
            }
            else if (Traveling && e.Floor == Destination)
            {
                e.Arrival -= OnArrival;
                Program.Statistics[Program.Hour]++;
                var waitTime = (Program.Hour - SpawnHour) * 60 + Program.Minute - SpawnMinute;
                if (waitTime > Program.MaxWaitTime)
                    Program.MaxWaitTime = waitTime;
            }
        }

        public delegate void OnBoard(Person p, Elevator e);
        public event OnBoard Board;
    }
}