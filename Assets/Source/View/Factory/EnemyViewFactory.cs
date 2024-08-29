using Model;
using UnityEngine;

namespace View
{
    public class EnemyViewFactory : ViewFactory<Enemy>
    {
        [SerializeField] private GameObject _defoltEnemy;

        private EnemyVisiter _visiter;

        public void Awake()
        {
            _visiter = new EnemyVisiter(_defoltEnemy);
        }

        protected override GameObject GetTemplay(Enemy prefab)
        {
            _visiter.Vist((dynamic)prefab);
            return _visiter.CurentEnemy;
        }

        public sealed class EnemyVisiter : IEnemyVisiter
        {
            private readonly GameObject _defoltEnemy;

            public GameObject CurentEnemy { get; private set; }

            public EnemyVisiter(GameObject defoltEnemy)
            {
                _defoltEnemy = defoltEnemy;
            }

            public void Vist(DefoltEnemy enemy)
            => CurentEnemy = _defoltEnemy;
        }
    }
}