using Menager;
using Model;
using UnityEngine;
using UnityEngine.UI;
using View;
using View.Panel;

public class TutorialMenager : MonoBehaviour
{
    [SerializeField] private TutorialMovemengView _movemengView;
    [SerializeField] private TutorialAttackView _attackView;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _camera;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private Button _attack;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PlayerConffig _playerConffig;
    [SerializeField] private PlayerMenager _playerMenager;

    private readonly Fsm _fsm = new Fsm();

    private void Awake()
    {
        InputRouter inputRouter = new InputRouter(_fixedJoystick,_attack,_characterController);
        Player player = new Player(_playerConffig, transform);
        PlayerController playerController = new PlayerController(_characterController, _camera, inputRouter, _playerAnimator, _playerTransform,player);
        _fsm.BindState(new TutorialMovemengState(() => Time.deltaTime, _movemengView, playerController, _fsm))
            .BindState(new TutorialAttackState(new EntityAnimator(_playerAnimator),() => Time.deltaTime,_movemengView, _attackView, playerController, _fsm))
            .BindState(new TutorialEndState(_fsm));

        _fsm.SetState<TutorialMovemengState>();

        _playerMenager.Init(_characterController, _camera, inputRouter, _playerAnimator, _playerTransform, player);
    }

    private void Update()
    => _fsm.Update();
}