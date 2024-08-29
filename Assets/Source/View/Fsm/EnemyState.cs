using Model;

namespace View
{
    public class EnemyState : FsmState
    {
        private readonly EnemyData _enemyData;

        public EnemyState(IEnemyData data, Fsm fsm) : base(fsm)
        {
            _enemyData = new EnemyData(data);
        }

        public override void Update()
        {
            if (_enemyData.IsEnd)
                Fsm.SetState<EnemyDestroyState>();
            else if (_enemyData.IsMove)
                Fsm.SetState<EnemyMovemegState>();
            else if (_enemyData.IsAttack)
                Fsm.SetState<EnemyAttakState>();
        }

        private class EnemyData
        {
            private readonly IEnemyData _enemyData;

            public bool IsMove => _enemyData.isMovemeng;
            public bool IsAttack => _enemyData.isAttack;
            public bool IsEnd => _enemyData.isEnd;

            public EnemyData(IEnemyData enemyData)
            {
                _enemyData = enemyData;
            }
        }
    }
}