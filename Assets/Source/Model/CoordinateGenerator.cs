using Model;
using UnityEngine;

public class CoordinateGenerator : IService
{
    private readonly IPosition _player;
    private readonly Transform _map;
    private readonly EnemyRegistry _allEnemy;
    private readonly int _originalMapLength;

    public CoordinateGenerator(EnemyRegistry enemyRegistry,Transform map,Player player, int originalMapLength)
    {
        _map = map;
        _player = player;
        _allEnemy = enemyRegistry;
        _originalMapLength = originalMapLength;
    }

    public Vector3 GenerateRandomPositionWithinBounds()
    {
        Vector3 min = _map.position - _map.localScale / 2 * _originalMapLength;
        Vector3 max = _map.position + _map.localScale / 2;
        Vector3 randomPosition = default;
        bool positionIsFree = true;

        while (positionIsFree)
        {
            positionIsFree = false;
            randomPosition = new Vector3(Random.Range(min.x, max.x), max.y, Random.Range(min.z, max.z));

            foreach (var enemy in _allEnemy.Enemys)
            {
                if(enemy.Position == randomPosition)
                    positionIsFree = true;
            }

            if (randomPosition == _player.Position)
                positionIsFree = true;
        }

        return randomPosition;
    }
}