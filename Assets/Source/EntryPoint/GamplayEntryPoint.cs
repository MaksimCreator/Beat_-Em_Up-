using Menager;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class GamplayEntryPoint : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerConffig _conffig;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _map;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private Button _gamplayButtonAttack;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerMenager _playerManager;
    [SerializeField] private EnemyMenager _enemyManager;
    [SerializeField] private GamplayMenager _gamplayManager;
    [SerializeField] private int _originalMapLength = 10;

    private readonly ServiceLocator _serviceLocator = new();

    public ServiceLocator ServiceLocator => _serviceLocator;

    private void Awake()
    {
        PlayerHealth health = new PlayerHealth(_conffig.MaxHealth,_conffig.MaxHealth);
        Player player = new Player(_conffig,_playerTransform);
        EnemyRegistry enemyRegistry = new EnemyRegistry();

        CoordinateGenerator generator = new CoordinateGenerator(enemyRegistry, _map, player, _originalMapLength);
        Wallet wallet = new Wallet();
        Mediator mediator = new Mediator(health, wallet);

        InputRouter router = new InputRouter(_fixedJoystick,_gamplayButtonAttack,_characterController);
        _gamplayManager.ServiceLocatorInitialized(_serviceLocator, enemyRegistry, generator, mediator, router);

        EnemySimulated simulated = new EnemySimulated();
        EnemyController enemyController = new EnemyController(new EnemySpawner(player, wallet, simulated, _serviceLocator), simulated, enemyRegistry);

        _playerManager.Init(_characterController,_mainCamera,router, _playerAnimator, _playerTransform, player);
        _enemyManager.Init(enemyController);
        _gamplayManager.Activate(player, health, _serviceLocator);
    }

    private void OnEnable()
    {
        _playerManager.Enable();
        _enemyManager.Enable();
        _gamplayManager.Enable();
    }
}
