using Model;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace View
{
    public abstract class ViewFactory<T> : MonoBehaviour, IService where T : Entity
    {
        [SerializeField] private GamplayEntryPoint _entryPointGamplay;

        private Dictionary<T, GameObject> _views = new();
        private PhysicsRouter _router;

        public void Creat(T model, Vector3 position, Quaternion rotation, Action<T, GameObject> action = null)
        {
            GameObject prefab = Instantiate(GetTemplay(model), position, rotation);
            _views.Add(model, prefab);
            TryInitRouter();

            Initialized.InitializedChildrenPhysicsEventBroadcaster(model,_router,prefab, model);
            Initialized.InitializedIgnoreCollider(prefab);

            TryEnterAction(action, prefab, model);
        }

        public void Destroy(T prefab)
        {
            GameObject model = _views[prefab];
            _views.Remove(prefab);
            Destroy(model);
        }

        protected abstract GameObject GetTemplay(T prefab);

        private void TryEnterAction(Action<T,GameObject> action,GameObject prefab,T model)
        {
            if (action != null)
                action(model,prefab);
        }

        private void TryInitRouter()
        {
            if (_router == null)
                _router = _entryPointGamplay.ServiceLocator.GetSevice<PhysicsRouter>();
        }
    }
}
namespace Model
{
    public static class Initialized
    {
        public static void InitializedChildrenPhysicsEventBroadcaster<T>(Entity entity,PhysicsRouter router,GameObject prefab, T model) where T : class
        {
            foreach (var broadcaster in prefab.GetComponentsInChildren<PhysicsEventBroadcaster>())
            {
                if (broadcaster.gameObject.layer == Constant.LayerFeet)
                    broadcaster.Init(new Feet(entity), router);
                else
                    broadcaster.Init(model, router);
            }
        }

        public static void InitializedIgnoreCollider(GameObject prefab)
        {
            Collider[] allCollider = prefab.GetComponentsInChildren<Collider>();

            for (int i = 0; i < allCollider.Length; i++)
            {
                for (int y = 0; y < allCollider.Length; y++)
                {
                    if (i == y)
                    {
                        y++;
                        if (y == allCollider.Length)
                            break;
                    }
                    Physics.IgnoreCollision(allCollider[i], allCollider[y]);
                }
            }
        }
    }
}