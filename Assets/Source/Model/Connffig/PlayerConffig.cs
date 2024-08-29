using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Conffig/PlayerConffig")]
    public class PlayerConffig : ScriptableObject, IService
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _maxHealth;

        public float Speed => _speed;
        public int MaxHealth => _maxHealth;
    }
}