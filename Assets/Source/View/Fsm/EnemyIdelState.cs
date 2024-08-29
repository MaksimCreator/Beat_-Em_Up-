using Model;

namespace View
{
    public class EnemyIdelState : EnemyState
    {
        private readonly EntityAnimator _animator;

        public EnemyIdelState(IEnemyData enemyData, Fsm fsm) : base(enemyData, fsm) 
        {
            _animator = new EntityAnimator(enemyData.Animator);
        }

        public override void Enter()
        => _animator.EnterIdelMovemeng();
    }
}