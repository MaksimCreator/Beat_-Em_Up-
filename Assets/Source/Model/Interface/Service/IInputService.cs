using UnityEngine;

namespace Model
{
    public interface IInputService : IService
    {
        Vector2 Direction { get; }
        bool IsMove();
        bool IsAttack();
        bool IsGrounded();
    }
}