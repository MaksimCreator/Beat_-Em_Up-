using System.Collections.Generic;
using static Model.PhysicsRouter;

namespace Model
{
    public sealed class CollisionRecord
    {
        public IEnumerable<Record> Values()
        {
            yield return new Record<Feet, Enemy>((feet, enemy) =>
            {
                enemy.TakaDamage(feet.Damage);
            });
        }
    }
}