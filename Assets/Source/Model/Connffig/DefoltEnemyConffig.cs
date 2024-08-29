using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Conffig/DefoltEnemy")]
    public class DefoltEnemyConffig : ScriptableObject
    {
        [SerializeField] private int _speed;
        [SerializeField] private int _maxHealth;

        public int Speed => _speed;
        public int MaxHealth => _maxHealth;
    }
}