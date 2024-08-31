using Model;

namespace View
{
    public class PlayerAttakState : FsmState
    {
        private readonly EntityAnimator _animator;
        private readonly IColliderControl _control;

        public PlayerAttakState(IColliderControl colliderControl,EntityAnimator animator, Fsm fsm) : base( fsm)
        {
            _animator = animator;
            _control = colliderControl;
        }

        public override void Enter()
        {
            _control.EnableColliders();
            _animator.EnterKick();
            Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Kick),Fsm.SetState<PlayerIdelAttackState>);
        }

        public override void Exit()
        => _control.DisableColliders();
    }
}