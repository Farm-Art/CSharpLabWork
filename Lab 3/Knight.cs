namespace Lab_3
{
    public class Knight : Character
    {
        protected int _strength;
        public int Strength => _strength;
        public new int Damage => _hitpower * (int)(_strength / 10.0);
        
        public Knight(int strength) : base(150, 20, 3)
        {
            _strength = strength;
        }

        public override string ToString()
        {
            return base.ToString() + $", STRENGTH = {Strength}";
        }
    }
}