using Model;

namespace View
{
    public class PlayerAttakState : FsmState
    {
        private readonly EntityAnimator _animator;

        public PlayerAttakState(EntityAnimator animator, Fsm fsm) : base( fsm)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.EnterKick();
            Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Kick),Fsm.SetState<PlayerIdelAttackState>);
        }
    }
}