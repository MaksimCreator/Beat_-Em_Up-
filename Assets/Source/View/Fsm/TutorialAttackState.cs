using Model;
using System;
using View.Panel;

namespace View
{
    public class TutorialAttackState : FsmState
    {
        private const int Time = 10;

        private readonly TutorialAttackView _tutorialAttackView;
        private readonly TutorialMovemengView _movemengView;
        private readonly PlayerController _playerController;
        private readonly Func<float> _getDelta;
        private readonly EntityAnimator _animator;

        private bool isInit;
        private bool isAttack = true;

        public TutorialAttackState(EntityAnimator animator,Func<float> GetDelta,TutorialMovemengView tutorialMovemengView, TutorialAttackView tutorialAttackView, PlayerController playerController, Fsm fsm) : base(fsm)
        {
            _tutorialAttackView = tutorialAttackView;
            _movemengView = tutorialMovemengView;
            _movemengView = tutorialMovemengView;
            _playerController = playerController;
            _getDelta = GetDelta;
            _animator = animator;

            _tutorialAttackView.Init(Enable);
        }

        public override void Enter()
        {
            _movemengView.Enable();
            _tutorialAttackView.Enable();
            _tutorialAttackView.EnableText();
            _movemengView.DisableText();
        }

        public override void Update() 
        => _playerController.Update(_getDelta.Invoke());

        private void Enable()
        {
            if (isAttack)
            {
                _animator.EnterKick();
                isAttack = false;
                Timer.StartTimer(_animator.GetLengchClip(ConstantAnimation.Kick), () => 
                {
                    isAttack = true;
                    _animator.EnterIdelMovemeng();
                });
            }

            if (isInit == false)
            {
                isInit = true;
                _tutorialAttackView.DisableText();
                Timer.StartTimer(Time, () =>
                {
                    _tutorialAttackView.Disable();
                    _playerController.Disable();
                    Fsm.SetState<TutorialEndState>();
                });
            }
        }
    }
}
