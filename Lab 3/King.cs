using System;

namespace Lab_3
{
    public class King : Character
    {
        protected int _maxArmySize;
        protected Character[] _army = null;

        public int ArmySize
        {
            get
            {
                int size = 0;
                foreach (Character chr in _army)
                    if (chr.IsAlive)
                        size++;
                return size;
            }
        }

        public int MaxArmySize => _maxArmySize;

        public King(int armySize = 5) : base(75, 5, 7)
        {
            _maxArmySize = armySize;
            _army = new Character[armySize];
            for (int i = 0; i < armySize; i++)
                _army[i] = new Knight(_rand.Next(0, 10));
        }

        public override void Fight(Character other, bool initiative = true)
        {
            bool hasArmy = false;
            foreach (Character chr in _army)
                if (chr.IsAlive)
                {
                    chr.Fight(other, false);
                    hasArmy = true;
                }

            if (hasArmy) return;
            // If the army is dead, the king fights himself
            base.Fight(other, initiative);
            _army = Array.Empty<Character>();
        }

        public override void TakeDamage(Character offender, int damage)
        {
            // The army tries to defend the king
            foreach (Character chr in _army)
                if (chr.IsAlive)
                {
                    chr.TakeDamage(offender, damage);
                    return;
                }
            // If the army is dead, the king takes damage
            base.TakeDamage(offender, damage);
        }

        public override string ToString()
        {
            return base.ToString() + $", MAXARMYSIZE = {MaxArmySize}, ARMYSIZE = {ArmySize}";
        }
    }
}