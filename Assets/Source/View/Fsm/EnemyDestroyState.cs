using Model;
using System;

namespace View
{
    public class EnemyDestroyState : FsmState
    {
        private readonly Action _onKill;
        private readonly EntityAnimator _animator;

        public EnemyDestroyState(Action onKill,EntityAnimator animator, Fsm fsm) : base(fsm)
        {
            _onKill = onKill;
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.EnterDeath();
            Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Death), () => 
            {
                _onKill();
                Fsm.SetState<EnemyIdelState>();
            });
        }
    }
}