using UnityEngine;

namespace Model
{
    public class Entity : IPosition
    {
        protected readonly float Speed;
        protected Transform _transform;

        public Vector3 Position => _transform.position;

        public Entity(float speed)
        {
            Speed = speed;
        }

        public void TryBindTransform(Transform transform)
        {
            if (_transform == null)
                _transform = transform;
        }
    }
}