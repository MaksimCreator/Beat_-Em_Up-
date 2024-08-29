namespace Model
{
    public class Wallet : IWalletData
    {
        private readonly EnemyVisiter _enemyVisiter = new();

        public int Score => _enemyVisiter.Score;

        public void OnKill(Enemy enemy)
        => _enemyVisiter.Vist((dynamic)enemy);

        private class EnemyVisiter : IEnemyVisiter
        {
            private int _accamulatedScore;

            public int Score => _accamulatedScore;

            public void Vist(DefoltEnemy enemy)
            => _accamulatedScore += 5;
        }
    }
}