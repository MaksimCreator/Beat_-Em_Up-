using Model;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace View
{
    public abstract class ViewFactory<T> : MonoBehaviour, IService where T : class
    {
        [SerializeField] private GamplayEntryPoint _entryPointGamplay;

        private Dictionary<T, GameObject> _views = new();
        private PhysicsRouter _router;

        public void Creat(T prefab, Vector3 position, Quaternion rotation, Action<T, GameObject> action = null)
        {
            GameObject model = Instantiate(GetTemplay(prefab), position, rotation);
            _views.Add(prefab, model);

            if (_router == null)
                _router = _entryPointGamplay.ServiceLocator.GetSevice<PhysicsRouter>();

            foreach (var broadcaster in model.GetComponentsInChildren<PhysicsEventBroadcaster>())
            {
                if (broadcaster.gameObject.layer == Constant.LayerFeet)
                    broadcaster.Init(new Feet(), _router);
                else
                    broadcaster.Init(prefab, _router);
            }

            if (action != null)
                action(prefab, model);
        }

        public void Destroy(T prefab)
        {
            GameObject model = _views[prefab];
            _views.Remove(prefab);
            Destroy(model);
        }

        protected abstract GameObject GetTemplay(T prefab);
    }
}