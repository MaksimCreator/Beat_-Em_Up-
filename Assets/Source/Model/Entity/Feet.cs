namespace Model
{
    public sealed class Feet
    {
        private readonly IColliderControl _entity;

        public int Damage { get; } = 20;
        
        public Feet(Entity entity)
        {
            _entity = entity;
        }

        public void DisableCollider()
        => _entity.DisableColliders();
    }
}