using UnityEngine;

namespace Model
{
    public abstract class Entity : IPosition,IColliderControl
    {
        private Collider[] _damageColliders;

        protected readonly float Speed;
        protected Transform _transform;

        public Vector3 Position => _transform.position;

        public Entity(float speed)
        {
            Speed = speed;
        }

        public Entity BindDamageCollider(Collider[] damageColliders)
        {
            _damageColliders = damageColliders;
            DisableColliders();
            return this;
        }

        public abstract void TakeDamage(float damage);

        public void TryBindTransform(Transform transform)
        {
            if (_transform == null)
                _transform = transform;
        }

        public void EnableColliders()
        {
            foreach(var collider in _damageColliders)
                collider.enabled = true;
        }

        public void DisableColliders()
        {
            foreach (var collider in _damageColliders)
            {
                if(collider != null)
                    collider.enabled = false;
            }
        }
    }
}