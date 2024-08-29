using System;

namespace Model
{
    public abstract class Health : IHealthEntityData
    {
        private const int _minHealth = 0;
        private readonly int _maxHealth;

        public int MaxHealth => _maxHealth;
        public int MinHealth { get; }
        public int CurentHealth { get; private set; }

        public Health(int maxHealth,int curentHealth)
        {
            _maxHealth = maxHealth;
            CurentHealth = curentHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new InvalidOperationException(nameof(damage));

            if (CurentHealth - damage <= MinHealth)
            {
                CurentHealth = 0;
                OnDeath();
            }
            else
            {
                CurentHealth -= damage;
            }

        }

        protected abstract void OnDeath();
    }
}