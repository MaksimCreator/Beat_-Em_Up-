using System;
using UnityEngine;

namespace Model
{
    public abstract class Enemy : Entity, IEnemyData
    {
        private const int _minDistanceUnit = 1;

        private readonly Health _health;
        private IPosition _targetPosition;
        private bool _isEnable;

        public event Action<Enemy> onDeath;

        public CharacterController CharacterController { get; private set; }

        public Animator Animator { get; private set; }

        public Vector3 TargetPosition => _targetPosition.Position;

        public Vector3 Direction { get; private set; }

        public bool isMovemeng { get; private set; }

        public bool isAttack { get; private set; }

        public bool isEnd { get; private set; }

        protected Enemy(float speed,Player player,Health health) : base(speed)
        {
            _targetPosition = player;
            _health = health;
            _health.onDeath += () => onDeath(this);
        }

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            Vector3 way = _targetPosition.Position - _transform.position;
            isAttack = IsAttack(way);

            if(isMovemeng == false)
                return;

            if (isAttack)
                return;

            Vector3 direction = way.normalized * Speed * delta;
            Direction = direction;
        }

        public void TryEnable()
        {
            if (_isEnable)
                return;

            _isEnable = true;
            onDeath += (enemy) => isEnd = true;
        }

        public void Disable()
        {
            if (_isEnable == false)
                return;

            _isEnable = false;
            isMovemeng = false;
            isAttack = false;
            isEnd = false;

            onDeath -= (enemy) => isEnd = true;
        }

        public void StartMovemeng(CharacterController characterControlle,Transform transform,Animator animator)
        {
            if (Animator == null)
                Animator = animator;

            CharacterController = characterControlle;
            TryBindTransform(transform);
            isMovemeng = true;
            isEnd = false;
        }

        public override void TakeDamage(float damage)
        => _health.TakeDamage((int)damage);

        public void Stop()
        {
            isMovemeng = false;
            isAttack = false;
            isEnd = true;
        }

        public void Start()
        {
            isEnd = false;
            isMovemeng = true;
        }

        private bool IsAttack(Vector3 way)
        {
            bool isAttack = way.magnitude <= _minDistanceUnit;
            isMovemeng = isAttack == false;
            return isAttack;
        }
    }
}
public interface IColliderControl
{
    void EnableColliders();
    void DisableColliders();
}