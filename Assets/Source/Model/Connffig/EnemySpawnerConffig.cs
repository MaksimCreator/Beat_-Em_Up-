using Model;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Conffig/EnemySpawner")]
    public sealed class EnemySpawnerConffig : ScriptableObject,IService
    {
        [SerializeField] private int _lengthBeforceSpawn;
        [SerializeField] private DefoltEnemyConffig _enemyConffig;

        public int LengthBeforceSpawn => _lengthBeforceSpawn;
        public DefoltEnemyConffig DefoltEnemy => _enemyConffig;
    }
}