using System;
using View.Panel;

namespace View
{
    public class TutorialMovemengState : FsmState
    {
        private const int TimeSecond = 10;

        private readonly TutorialMovemengView _tutorialMovemengView;
        private readonly PlayerController _playerController;
        private readonly Func<float> _getDelta;

        public TutorialMovemengState(Func<float> getDelta,TutorialMovemengView tutorialMovemengView,PlayerController playerController,Fsm fsm) : base(fsm)
        {
            _getDelta = getDelta;
            _tutorialMovemengView = tutorialMovemengView;
            _playerController = playerController;
        }

        public override void Enter()
        {
            _tutorialMovemengView.EnableText();
            _tutorialMovemengView.Enable();
            _playerController.Enable();
            Timer.StartTimer(TimeSecond, () =>
            {
                _tutorialMovemengView.DisableText();
                Fsm.SetState<TutorialAttackState>();
            });
        }

        public override void Update()
        => _playerController.Update(_getDelta.Invoke());
    }
}
