using System;
using UnityEngine;

namespace Model
{
    public abstract class Enemy : Entity, IEnemyData
    {
        private const int _minDistanceUnit = 1;

        private IPosition _targetPosition;
        private bool _isEnable;

        public CharacterController CharacterController { get; private set; }

        public EnemyHealth Health { get; private set; }

        public Animator Animator { get; private set; }

        public Vector3 TargetPosition => _targetPosition.Position;

        public Vector3 Direction { get; private set; }

        public bool isMovemeng { get; private set; }

        public bool isAttack { get; private set; }

        public bool isEnd { get; private set; }

        protected Enemy(float speed,Player player) : base(speed)
        {
            _targetPosition = player;
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
            Health.onDeath += (enemy) => isEnd = false;
        }

        public void Disable()
        {
            if (_isEnable == false)
                return;

            _isEnable = false;
            Health.onDeath -= (enemy) => isEnd = false;
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

        public Enemy BindEnemyHealth(EnemyHealth health)
        { 
            Health = health;
            return this;
        }

        public void TakaDamage(int damage)
        => Health.TakeDamage(damage);

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