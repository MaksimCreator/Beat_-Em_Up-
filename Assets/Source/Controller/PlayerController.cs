using Model;
using UnityEngine;
using View;

public class PlayerController
{
    private readonly Fsm _fsmMovemeng = new();
    private readonly Fsm _fsmAttack = new();
    private readonly PlayerMovemeng _PlayerMovemeng;
    private readonly InputRouter _router;
    private readonly CameraMovement _cameraMovemeng;

    public PlayerController(CharacterController characterController,Camera camera,InputRouter inputRouter,Animator animator,Transform transformPlayer,Player player)
    {
        _PlayerMovemeng = new PlayerMovemeng(inputRouter, player);
        _cameraMovemeng = new CameraMovement(camera.transform,transformPlayer);
        _router = inputRouter;

        EntityAnimator EntityAnimator = new EntityAnimator(animator);

        _fsmMovemeng.BindState(new PlayerIdelMovemengState(characterController,inputRouter, player,EntityAnimator, _fsmMovemeng))
            .BindState(new PlayerMovemengState(characterController, inputRouter, player, EntityAnimator, _fsmMovemeng));

        _fsmAttack.BindState(new PlayerIdelAttackState(inputRouter,EntityAnimator,_fsmAttack))
            .BindState(new PlayerAttakState(EntityAnimator,_fsmAttack));

        _fsmMovemeng.SetState<PlayerIdelMovemengState>();
        _fsmAttack.SetState<PlayerIdelAttackState>();
    }

    public void Update(float delta)
    {
        _PlayerMovemeng.Update(delta);
        _cameraMovemeng.Update();
        _fsmAttack.Update();
        _fsmMovemeng.Update();
    }

    public void Enable()
    => _router.Enable();

    public void Disable()
    => _router.Disable();
}
