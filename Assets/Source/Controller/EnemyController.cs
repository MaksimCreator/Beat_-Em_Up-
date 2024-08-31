using Model;
using System.Collections.Generic;
using View;

public class EnemyController
{
    private readonly EnemySimulated _simulated;
    private readonly EnemySpawner _spawner;
    private readonly EnemyRegistry _registry;

    private readonly Dictionary<IEnemyData, IUpdateble> _enemys = new();

    public EnemyController(EnemySpawner spawner,EnemySimulated simulated, EnemyRegistry registry)
    {
        _simulated = simulated;
        _simulated.SwithAction(AddEnemy);

        _registry = registry;
        _spawner = spawner;
    }

    public void Update(float delta)
    {
        foreach (var fsm in _enemys.Values)
            fsm.Update();

        _simulated.Update(delta);
        _spawner.Update(delta);
    }

    public void Enable()
    => _simulated.Start();

    public void Disable()
    => _simulated.Stop();

    public void AllStop()
    => _simulated.AllStop();

    private void AddEnemy(IEnemyData enemyData)
    {
        if (_enemys.TryGetValue(enemyData, out IUpdateble fsmUpdatable) != false)
            return;

        Enemy enemy = enemyData as Enemy;
        Fsm fsm = new Fsm();
        _registry.AddEnemy(enemy);

        fsm.BindState(new EnemyIdelState(enemy, fsm))
            .BindState(new EnemyMovemegState(enemy,fsm))
            .BindState(new EnemyAttakState(enemy,new EntityAnimator(enemy.Animator),fsm))
            .BindState(new EnemyDestroyState(() => _spawner.Disable(enemy),new EntityAnimator(enemy.Animator),fsm));

        fsm.SetState<EnemyIdelState>();

        _enemys.Add(enemy, fsm);
    }
}