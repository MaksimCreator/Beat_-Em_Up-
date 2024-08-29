namespace Model
{
    public class Mediator : IMediatorData
    {
        private readonly IHealthEntityData _healthData;
        private readonly IWalletData _walletData;

        public Mediator(IHealthEntityData healthData,IWalletData walletData)
        {
            _healthData = healthData;
            _walletData = walletData;
        }

        public int MaxHealth => _healthData.MaxHealth;
        public int CurentHealth => _healthData.CurentHealth;
        public int MinHealth => _healthData.MinHealth;
        public int Score => _walletData.Score;
    }
}