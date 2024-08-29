using UnityEngine;

namespace Model
{
    public interface IEnemyData
    {
        CharacterController CharacterController { get; }
        Animator Animator { get; }
        Vector3 Direction { get; }
        Vector3 TargetPosition { get; }
        bool isMovemeng { get; }
        bool isAttack { get; }
        bool isEnd { get; }
    }
}