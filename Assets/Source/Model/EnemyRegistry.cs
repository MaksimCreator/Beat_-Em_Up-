using Model;
using System.Collections.Generic;

public class EnemyRegistry : IService
{
    private readonly HashSet<IPosition> _allEnemy = new();

    public IEnumerable<IPosition> Enemys => _allEnemy;

    public void AddEnemy(Enemy enemy)
    => _allEnemy.Add(enemy);
}