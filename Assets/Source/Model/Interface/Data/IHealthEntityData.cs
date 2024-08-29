namespace Model
{
    public interface IHealthEntityData : IService
    {
        public int MinHealth { get; }
        public int MaxHealth { get; }
        public int CurentHealth { get; }
    }
}