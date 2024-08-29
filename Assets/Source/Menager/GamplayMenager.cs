using View;
using Model;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Menager
{
    public class GamplayMenager : MonoBehaviour
    {
        [SerializeField] private PlayerMenager _playerManager;
        [SerializeField] private EnemyMenager _enemyManager;
        [SerializeField] private EnemyViewFactory _enemyViewFactory;
        [SerializeField] private EnemySpawnerConffig _enemySpawnerConffig;
        [SerializeField] private PlayerConffig _playerConffig;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private ExitMenuView _exitMenuView;
        [SerializeField] private GamplayMenuView _gamplayMenuView;
        [SerializeField] private List<PhysicsEventBroadcaster> _player;
        [SerializeField] private List<PhysicsEventBroadcaster> _feetListPlayer;

        private Fsm _fsm;

        private bool _isActivate;

        public void ServiceLocatorInitialized(ServiceLocator locator,EnemyRegistry registry,CoordinateGenerator generator,Mediator mediator,InputRouter router)
        => ServiceInitialized.RegisterService(locator, registry, generator, _enemyViewFactory, mediator, _playerConffig, _enemySpawnerConffig, router);

        public void Activate(Player player,PlayerHealth health,ServiceLocator locator)
        {
            Physics.IgnoreLayerCollision(Constant.LayerIgnoreCollisionPathEntity,Constant.LayerPathEntity);

            if (_fsm == null)
            {
                _fsm = new Fsm();
                _fsm.BindState(new GUIInitializedState(this, health, locator.GetSevice<Mediator>(), _endGameView, _exitMenuView, _gamplayMenuView, _fsm))
                    .BindState(new PhysicsRoutingState(player,_feetListPlayer,_player,this, locator.GetSevice<PhysicsRouter>(), _fsm))
                    .BindState(new IdelState(_fsm));
            }

            _fsm.SetState<GUIInitializedState>();
            _isActivate = true;
            enabled = true;
        }

        public void Enable()
        {
            if (_isActivate == false)
                throw new InvalidOperationException();

            OnEnable();
        }

        public void Disable()
        => OnDisable();

        public void AllStop()
        {
            _playerManager.AllStop();
            _enemyManager.AllStop();
            _fsm.SetState<IdelState>();
            _isActivate = false;
        }

        private void OnEnable()
        {
            _playerManager.Enable();
            _enemyManager.Enable();
        }

        private void OnDisable()
        {
            _playerManager.Disable();
            _enemyManager.Disable();
        }
    }
}
