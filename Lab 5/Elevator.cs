using System.Collections.Generic;

namespace Lab_5
{
    public class Elevator
    {
        private bool _up = true;
        private int _minFloor, _maxFloor;
        public int Floor { get; private set; }
        public List<Person> Contents { get; }

        public Elevator(int min, int max, int curr)
        {
            _minFloor = min;
            _maxFloor = max;
            Floor = curr;
            Contents = new List<Person>();
        }

        public void Move()
        {
            if (Floor == _minFloor)
                _up = true;
            else if (Floor == _maxFloor)
                _up = false;
            Floor += (_up ? 1 : -1);
            Arrival?.Invoke(this);
        }

        public delegate void OnArrival(Elevator sender);
        public event OnArrival Arrival;
    }
}