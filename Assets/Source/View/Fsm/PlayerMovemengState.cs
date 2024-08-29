using Model;
using UnityEngine;

namespace View
{
    public class PlayerMovemengState : FsmState
    {
        private readonly PlayerData _playerData;
        private readonly CharacterController _characterController;
        private readonly EntityAnimator _animator;
        private readonly Transform _playerTransform;

        public PlayerMovemengState(CharacterController characterController,IInputService inputService,IPlayerData playerData,EntityAnimator animator, Fsm fsm) : base(fsm)
        {
            _playerData = new PlayerData(inputService,playerData);
            _playerTransform = characterController.transform;
            _characterController = characterController;
            _animator = animator;
        }

        public override void Enter()
        => _animator.EnterRun();

        public override void Update()
        {
            if (_playerData.IsMove == false)
            {
                Fsm.SetState<PlayerIdelMovemengState>();
                return;
            }

            _characterController.Move(_playerData.Direction);
           _playerTransform.rotation = _playerData.Rotation;
        }

        private class PlayerData
        {
            private readonly IPlayerData _playerData;
            private readonly IInputService _inputService;

            public Quaternion Rotation => _playerData.Rotation;
            public Vector3 Direction => _playerData.Direction;
            public bool IsMove => _inputService.IsMove();

            public PlayerData(IInputService inputService, IPlayerData playerData)
            {
                _inputService = inputService;
                _playerData = playerData;
            }
        }
    }
}