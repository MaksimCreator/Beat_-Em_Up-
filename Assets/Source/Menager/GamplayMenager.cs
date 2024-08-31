using View;
using Model;
using System;
using UnityEngine;

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
        [SerializeField] private GameObject _playerPrefab;

        private Fsm _fsm;
        private bool _isInitialized;

        public void ServiceLocatorInitialized(ServiceLocator locator,EnemyRegistry registry,CoordinateGenerator generator,Mediator mediator,InputRouter router)
        => ServiceInitialized.RegisterService(locator, registry, generator, _enemyViewFactory, mediator, _playerConffig, _enemySpawnerConffig, router);

        public void Init(Player player,Health health,ServiceLocator locator)
        {
            if (_fsm == null)
            {
                _fsm = new Fsm();
                _fsm.BindState(new GUIInitializedState(this, health, locator.GetSevice<Mediator>(), _endGameView, _exitMenuView, _gamplayMenuView, _fsm))
                    .BindState(new PhysicsRoutingState(player,_playerPrefab,this, locator.GetSevice<PhysicsRouter>(), _fsm))
                    .BindState(new IdelState(_fsm));
            }

            _fsm.SetState<GUIInitializedState>();
            _isInitialized = true;
        }

        public void Enable()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException();

            enabled = true;
        }

        public void Disable()
        => enabled = false;

        public void AllStop()
        {
            _playerManager.AllStop();
            _enemyManager.AllStop();
            _fsm.SetState<IdelState>();
            _isInitialized = false;
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
