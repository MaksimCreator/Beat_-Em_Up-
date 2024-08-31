using Model;
using UnityEngine.SceneManagement;
using Menager;

namespace View
{
    public class GUIInitializedState : FsmState
    {
        private readonly GamplayMenuView _gamplayMenuView;
        private readonly ExitMenuView _exitMenuView;
        private readonly EndGameView _endGameView;
        private readonly Mediator _mediator;
        private readonly Health _health;
        private readonly GamplayMenager _gamplayManager;

        public GUIInitializedState(GamplayMenager manager,Health health,Mediator mediator,EndGameView endGameView,ExitMenuView exitMenuView,GamplayMenuView gamplayMenuView,Fsm fsm) : base(fsm)
        {
            _health = health;
            _mediator = mediator;
            _gamplayManager = manager;
            _endGameView = endGameView;
            _exitMenuView = exitMenuView;
            _gamplayMenuView = gamplayMenuView;
        }

        public override void Enter()
        {
            _gamplayMenuView.Init(() =>
            {
                _exitMenuView.Enable(_mediator.Score);
                _gamplayManager.Disable();
            },_mediator);

            _exitMenuView.Init(() => SceneManager.LoadScene(Constant.StartMenuScenes), () =>
            {
                _gamplayMenuView.Enable();
                _gamplayManager.Enable();
            });

            _health.onDeath += OnGameEnd;
            Fsm.SetState<PhysicsRoutingState>();
        }

        private void OnGameEnd()
        {
            DataSave saveService = new DataSave();
            Data data = saveService.Load();

            int score = _mediator.Score;
            data = data == null ? new Data() : data;
            data.MyBest = score < data.MyBest ? data.MyBest : score;

            _endGameView.Enable(score);
            _gamplayManager.AllStop();
            _gamplayMenuView.Disable();
            _health.onDeath -= OnGameEnd;
        }
    }
}
