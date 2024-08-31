using Model;

namespace View
{
    public class EnemyAttakState : FsmState
    {
        private readonly EntityAnimator _animator;
        private readonly IColliderControl _colliderControl;

        public EnemyAttakState(IColliderControl colliderControl,EntityAnimator animator, Fsm fsm) : base(fsm) 
        {
            _animator = animator;
            _colliderControl = colliderControl;
        }

        public override void Enter()
        {
            _colliderControl.EnableColliders();
            _animator.EnterKick();
            Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Kick), Fsm.SetState<EnemyIdelState>);
        }

        public override void Exit()
        => _colliderControl.DisableColliders();
    }
}