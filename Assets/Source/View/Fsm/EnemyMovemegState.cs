using Model;
using UnityEngine;

namespace View
{
    public class EnemyMovemegState : EnemyState
    {
        private readonly IEnemyData _enemyData;
        private readonly CharacterController _enemyTransfom;
        private readonly Transform _transfomEnemy;
        private readonly EntityAnimator _animator;

        public EnemyMovemegState(IEnemyData data, Fsm fsm) : base(data, fsm)
        {
            _enemyData = data;
            _enemyTransfom = data.CharacterController;
            _transfomEnemy = data.CharacterController.transform;
            _animator = new EntityAnimator(data.Animator);
        }

        public override void Enter()
        => _animator.EnterRun();

        public override void Update()
        {
            _enemyTransfom.Move(_enemyData.Direction);
            _transfomEnemy.LookAt(_enemyData.TargetPosition);

            base.Update();
        }
    }
}