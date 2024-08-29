using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    public class InputRouter : IInputService
    {
        private readonly CharacterController _characterController;
        private readonly FixedJoystick _joystick;
        private readonly Button _attack;

        private bool _isAttack;

        public Vector2 Direction => _joystick.Direction;

        public InputRouter(FixedJoystick joystick, Button attack,CharacterController controller)
        {
            _joystick = joystick;
            attack.onClick.AddListener(() => _isAttack = true);
            _characterController = controller;
        }

        public bool IsMove()
        => Direction != Vector2.zero;

        public bool IsAttack()
        { 
            if (_isAttack)
            {
                _isAttack = false;
                return true;
            }

            return false;
        }

        public void Enable()
        => _joystick.enabled = true;

        public void Disable()
        => _joystick.enabled = false;

        public bool IsGrounded()
        => _characterController.isGrounded;
    }
}