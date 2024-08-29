using System;

namespace Model
{
    public class EnemyHealth : Health
    {
        public event Action<Enemy> onDeath;

        private Enemy _curentEnemy;

        public EnemyHealth(int maxHealth, int curentHealth) : base(maxHealth, curentHealth) { }

        public void BindEnemy(Enemy enemy)
        {
            if (_curentEnemy == null)
                _curentEnemy = enemy;
        }

        protected override void OnDeath()
        {
            if (_curentEnemy == null)
                throw new InvalidOperationException("Переменная _curentEnemy не про инацелезированна!");

            onDeath.Invoke(_curentEnemy);
        }
    }
}