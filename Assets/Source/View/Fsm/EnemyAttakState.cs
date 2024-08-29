using Model;

namespace View
{
    public class EnemyAttakState : FsmState
    {
        private readonly EntityAnimator _animator;

        public EnemyAttakState(EntityAnimator animator, Fsm fsm) : base(fsm) 
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.EnterKick();
            Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Kick), Fsm.SetState<EnemyIdelState>);
        }
    }
}