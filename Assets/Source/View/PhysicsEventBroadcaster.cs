using Model;
using UnityEngine;

public class PhysicsEventBroadcaster : MonoBehaviour
{
    private object _model;
    private PhysicsRouter _router;

    public void Init(object model, PhysicsRouter router)
    {
        _model = model;
        _router = router;
    }

    private void OnCollisionEnter(Collision collision)
    => TryBindCollision(collision.gameObject);

    private void TryBindCollision(GameObject model)
    {
        if (model.TryGetComponent(out PhysicsEventBroadcaster broadcaster))
            _router.TryAddCollision(_model, broadcaster._model);
    }
}
