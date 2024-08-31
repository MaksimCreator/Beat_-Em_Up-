using Model;
using UnityEngine;
using System.Collections;

namespace View
{
    public class PhysicsRoutingState : FsmState
    {
        private readonly PhysicsRouter _router;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly GameObject _prefab;
        private readonly Player _player;

        public PhysicsRoutingState(Player player,GameObject prefab, MonoBehaviour monoBehaviour, PhysicsRouter router,Fsm fsm) : base(fsm)
        {
            _prefab = prefab;
            _player = player;
            _monoBehaviour = monoBehaviour;
            _router = router;
        }

        public override void Enter()
        {
            Initialized.InitializedChildrenPhysicsEventBroadcaster(_player,_router, _prefab, _player);
            Initialized.InitializedIgnoreCollider(_prefab);

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
