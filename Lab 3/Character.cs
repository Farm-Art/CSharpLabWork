using System;

namespace Lab_3
{
    public abstract class Character
    {
        protected static Random _rand = new Random();
        protected int _health;
        protected int _hitpower;
        protected int _hitThreshold;

        public int Health => _health;
        public int HitPower => _hitpower;
        // Having a damage property allows easier alteration in subclasses
        // (see Knight.cs for an example)
        public int Damage => _hitpower;
        public bool IsAlive => Health > 0;

        protected Character(int health = 100, int hitpower = 15, int hitThreshold = 5)
        {
            _health = health;
            _hitpower = hitpower;
            _hitThreshold = hitThreshold;
        }
        
        public virtual void Fight(Character other, bool initiative = true)
        {
            double multiplier = 1;
            int roll;
            if (initiative)
            {
                // Attacker's advantage, guaranteed hit + 50% buff
                roll = 10;
                multiplier += 0.5;
            }
            else
                roll = _rand.Next(0, 10);
            
            if (roll >= _hitThreshold)
                other.TakeDamage(this, (int)(Damage * multiplier));
            
            // If they weren't killed, they'll fight back. Initiative
            // is required to trigger a response, to avoid running an
            // endless loop.
            if (other.IsAlive && initiative)
                other.Fight(this, false);
        }

        public virtual void TakeDamage(Character offender, int damage)
        {
            _health -= damage;
        }

        public override string ToString()
        {
            return $"{GetType()} stats: HEALTH = {Health}, HITPOWER = {HitPower}";
        }
    }
}