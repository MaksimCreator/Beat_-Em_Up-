using System;
using System.Collections.Generic;

namespace Model
{
    public class EnemySimulated : Simulated<IEnemyData, Enemy>
    {
        private Action<IEnemyData> _addNewEnemy;

        public EnemySimulated()
        {
            OnDistroy += Destroy;
            onStart += StartSimulate;
            onStop += StopSimulate;
        }

        public void SwithAction(Action<IEnemyData> addNewEnemy)
        => _addNewEnemy = addNewEnemy;

        public void Simulate(Enemy enemy)
        {
            TryAddEntity(enemy, enemy);
            _addNewEnemy.Invoke(enemy);
            enemy.TryEnable();
        }

        protected override void onUpdate(float delta)
        {
            foreach (var enemy in Entitys)
                enemy.Update(delta);
        }

        private void StartSimulate()
        {
            foreach (var enemy in Entitys)
                enemy.Start();
        }

        private void StopSimulate()
        {
            foreach (var enemy in Entitys)
                enemy.Stop();
        }

        private void Destroy(IEnumerable<Enemy> enemys)
        {
            foreach (var enemy in enemys)
                enemy.Disable();

            OnDistroy -= Destroy;
            onStart -= StartSimulate;
            onStop -= StopSimulate;
        }
    }
}