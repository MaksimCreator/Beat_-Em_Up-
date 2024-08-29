using System;
using System.Collections.Generic;

public abstract class Simulated<T1,T2>
{
    private readonly Dictionary<T1, T2> _dataEntityPairs = new();
    private readonly HashSet<T2> _entitys = new();
    private bool _isActive = true;

    public event Action<IEnumerable<T2>> OnDistroy;
    public event Action onStop;
    public event Action onStart;

    protected IEnumerable<T2> Entitys => _entitys;

    public void Stop()
    {
        _isActive = false;
        onStop?.Invoke();
    }

    public void Start()
    {
        _isActive = true;
        onStart?.Invoke();
    }

    public void Update(float delta)
    {
        if (delta <= 0)
            throw new InvalidOperationException();

        if (_isActive == false)
            return;

        onUpdate(delta);
    }

    public void AllStop()
    {
        OnDistroy.Invoke(_entitys);
        _entitys.Clear();
    }

    protected void TryAddEntity(T1 entityData, T2 entity)
    {
        if (_entitys.Add(entity))
            _dataEntityPairs.Add(entityData, entity);
    }

    protected T2 GetEntity(T1 interfaceEntity)
    => _dataEntityPairs[interfaceEntity];

    protected bool CanSimulated()
    => _isActive;

    protected abstract void onUpdate(float delta);
}
