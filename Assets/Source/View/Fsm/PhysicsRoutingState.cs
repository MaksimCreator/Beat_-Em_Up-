using Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace View
{
    public class PhysicsRoutingState : FsmState
    {
        private readonly PhysicsRouter _router;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly List<PhysicsEventBroadcaster> _playerPath;
        private readonly List<PhysicsEventBroadcaster> _feetList;
        private readonly Player _player;

        public PhysicsRoutingState(Player player,List<PhysicsEventBroadcaster> feetList, List<PhysicsEventBroadcaster> playerPhysicsEventBroadcaster, MonoBehaviour monoBehaviour, PhysicsRouter router,Fsm fsm) : base(fsm)
        {
            _playerPath = playerPhysicsEventBroadcaster;
            _feetList = feetList;
            _monoBehaviour = monoBehaviour;
            _router = router;
        }

        public override void Enter()
        {
            foreach (var item in _playerPath)
                item.Init(_player, _router);

            foreach (var item in _feetList)
                item.Init(new Feet(), _router);

            _monoBehaviour.StartCoroutine(GetRouterSteper());
        }

        public override void Exit()
        => _monoBehaviour.StopCoroutine(GetRouterSteper());

        private IEnumerator GetRouterSteper()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                _router.Step();
            }
        }
    }
}
