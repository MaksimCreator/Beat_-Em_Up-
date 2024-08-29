using Model;
using UnityEngine;
using System;
using System.Collections.Generic;
using View;

public class EnemySpawner
{
    private readonly Func<Enemy> _variants;
    private readonly Player _player;
    private readonly EnemySpawnerConffig _conffig;
    private readonly Wallet _wallet;
    private readonly EnemySimulated _enemySimulated;
    private readonly EnemyViewFactory _enemyViewFactory;
    private readonly EnemyVisiter _enemyVisiter;
    private readonly CoordinateGenerator _coordinateGenerator;

    private float _timer;
    private bool _isUpdate = true;

    public EnemySpawner(Player player,Wallet wallet,EnemySimulated simulated, ServiceLocator locator)
    {
        _player = player;
        _wallet = wallet;
        _enemySimulated = simulated;
        _conffig = locator.GetSevice<EnemySpawnerConffig>();
        _enemyViewFactory = locator.GetSevice<EnemyViewFactory>();
        _coordinateGenerator = locator.GetSevice<CoordinateGenerator>();

        _enemyVisiter = new EnemyVisiter(Instantiate);
        _variants = CreatDefoltEnemy;
        _enemySimulated.onStart += Enable;
        _enemySimulated.onStop += Disable;
        _enemySimulated.OnDistroy += AllStop;
    }

    public void Update(float delta)
    {
        if (delta <= 0)
            throw new InvalidOperationException(nameof(delta));

        if (_isUpdate == false)
            return;

        _timer += delta;

        if (_timer >= _conffig.LengthBeforceSpawn)
        {
            _timer = 0;
            EnableEnemy();
        }
    }

    public void Disable(Enemy enemy)
    {
        _enemyVisiter.Vist((dynamic)enemy);
        _enemyVisiter.CurentPoolObject.Disable(enemy);
    }

    private void EnableEnemy()
    {
        Vector3 RandomPosition = _coordinateGenerator.GenerateRandomPositionWithinBounds();
        Enemy enemy = _variants.Invoke();
        _enemyVisiter.Vist((dynamic)enemy);
        (Enemy,GameObject) pair = _enemyVisiter.CurentPoolObject.Enable(enemy, RandomPosition, Quaternion.identity);
        enemy = pair.Item1;
        GameObject gameObject = pair.Item2;
        enemy.StartMovemeng(gameObject.GetComponent<CharacterController>(),gameObject.transform, gameObject.GetComponent<Animator>());
        _enemySimulated.Simulate(enemy);
    }

    private void Enable()
    => _isUpdate = true;

    private void Disable()
    => _isUpdate = false;

    private void AllStop(IEnumerable<Enemy> enemys)
    {
        foreach (var enemy in enemys)
        {
            Subscribe(enemy.Health,SubscriptionType.Unsubscribe);
            _enemyViewFactory.Destroy(enemy);
        }

        _enemySimulated.onStart -= Enable;
        _enemySimulated.onStop -= Disable;
        _enemySimulated.OnDistroy -= AllStop;
    }

    private DefoltEnemy CreatDefoltEnemy()
    {
        EnemyHealth enemyHealth = new EnemyHealth(_conffig.DefoltEnemy.MaxHealth, _conffig.DefoltEnemy.MaxHealth);
        Enemy enemy = new DefoltEnemy(_conffig.DefoltEnemy.Speed,_player)
            .BindEnemyHealth(enemyHealth);

        Subscribe(enemyHealth, SubscriptionType.Subscribe);
        return enemy as DefoltEnemy;
    }

    private void Subscribe(EnemyHealth health,SubscriptionType type)
    {
        if (type == SubscriptionType.Subscribe)
        {
            health.onDeath += _wallet.OnKill;
        }
        else
        {
            health.onDeath -= _wallet.OnKill;
        }
    }

    private void Instantiate(Enemy enemy, Vector3 position, Quaternion rotation)
    => _enemyViewFactory.Creat(enemy, position, rotation, _enemyVisiter.CurentPoolObject.AddObject);

    private enum SubscriptionType
    {
        Subscribe,
        Unsubscribe
    }

    private class EnemyVisiter : IEnemyVisiter
    {
        private readonly PoolObject<Enemy> _poolObjectDefoltEnemy = new();

        public PoolObject<Enemy> CurentPoolObject { get; private set; }

        public EnemyVisiter(Action<Enemy, Vector3, Quaternion> instantiat)
        {
            _poolObjectDefoltEnemy.onInstantiat += instantiat;
        }

        public void Vist(DefoltEnemy enemy)
        => CurentPoolObject = _poolObjectDefoltEnemy;
    }
}