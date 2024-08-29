using System;
using UnityEngine;

namespace Model
{
    public sealed class PlayerMovemeng
    {
        private const float _gravity = 9.8f;

        private readonly Player _player;
        private readonly PlayerData _data;

        public PlayerMovemeng(IInputService input,Player player)
        {
            _player = player;
            _data = new PlayerData(input);
        }

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            Vector2 rotation = _data.Dierction.normalized * delta;
            Vector3 deltaDirection = _data.IsGrounded ? new Vector3(-rotation.y, 0, rotation.x) : new Vector3(-rotation.y, -_gravity * delta, rotation.x);

            _player.Move(deltaDirection);
            _player.Rotate(rotation);
        }

        private class PlayerData
        {
            private readonly IInputService _input;

            public PlayerData(IInputService input)
            {
                _input = input;
            }

            public Vector2 Dierction => _input.Direction;
            public bool IsGrounded => _input.IsGrounded();
            public bool IsMove => _input.IsMove();
        }
    }
}