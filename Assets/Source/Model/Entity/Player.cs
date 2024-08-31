using UnityEngine;
using System.Linq;

namespace Model
{
    public class Player : Entity,IPlayerData
    {
        private Health _health;

        public Quaternion Rotation { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool IsMove => Direction != Vector3.zero;

        public Player(PlayerConffig conffig,Transform transform) : base(conffig.Speed) 
        {
            TryBindTransform(transform);
            BindDamageCollider(transform.gameObject.GetComponentsInChildren<Collider>().Where(broadcaster => broadcaster.gameObject.layer == Constant.LayerFeet).ToArray());
        }

        public void Rotate(Vector2 rotation)
        => Rotation = Quaternion.LookRotation(new Vector3(-rotation.y, 0,rotation.x));

        public void Move(Vector3 deltaDirection)
        => Direction = new Vector3(deltaDirection.x * Speed,deltaDirection.y,deltaDirection.z * Speed);

        public Player BindHealth(Health health)
        {
            _health = health;
            return this;
        }

        public override void TakeDamage(float damage)
        => _health.TakeDamage((int)damage);
    }
}