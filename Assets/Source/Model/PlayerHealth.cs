using System;

namespace Model
{
    public class PlayerHealth : Health
    {
        public event Action onDeath;

        public PlayerHealth(int maxHealth, int curentHealth) : base(maxHealth, curentHealth) { }

        protected override void OnDeath()
        => onDeath.Invoke();
    }
}