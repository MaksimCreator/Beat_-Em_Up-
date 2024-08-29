using Model;
using UnityEngine;

namespace View
{
    public class PlayerIdelMovemengState : FsmState
    {
        private readonly PlayerData _playerData;
        private readonly EntityAnimator _animator;
        private readonly CharacterController _characterController;

        public PlayerIdelMovemengState(CharacterController characterController,IInputService inputService,IPlayerData playerData,EntityAnimator entityAnimation,Fsm fsm) : base(fsm)
        {
            _playerData = new PlayerData(inputService,playerData);
            _animator = entityAnimation;
            _characterController = characterController;
        }

        public override void Enter()
        => _animator.EnterIdelMovemeng();

        public override void Update()
        {
            _characterController.Move(new Vector3(0,_playerData.Gravity,0));

            if (_playerData.IsMove)
                Fsm.SetState<PlayerMovemengState>();
        }

        private class PlayerData
        {
            private readonly IInputService _inputService;
            private readonly IPlayerData _playerData;

            public bool IsMove => _inputService.IsMove();
            public float Gravity => _playerData.Direction.y;

            public PlayerData(IInputService inputService,IPlayerData playerData)
            {
                _inputService = inputService;
                _playerData = playerData;
            }
        }
    }
}