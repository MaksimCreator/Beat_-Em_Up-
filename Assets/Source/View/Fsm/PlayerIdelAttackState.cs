using Model;

namespace View
{
    public class PlayerIdelAttackState : FsmState
    {
        private readonly EntityAnimator _animator; 
        private readonly PlayerData _playerData;

        public PlayerIdelAttackState(IInputService inputService,EntityAnimator animator, Fsm fsm) : base(fsm)
        {
            _playerData = new PlayerData(inputService);
            _animator = animator;
        }

        public override void Enter()
        => _animator.EnterIdelMovemeng();

        public override void Update()
        {
            if (_playerData.IsAttack)
                Fsm.SetState<PlayerAttakState>();
        }

        private class PlayerData
        {
            private readonly IInputService _inputService;
        
            public bool IsAttack => _inputService.IsAttack();

            public PlayerData(IInputService inputService) 
            {
                _inputService = inputService;
            }
        }
    }
}