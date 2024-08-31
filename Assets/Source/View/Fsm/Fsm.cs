using System;
using System.Collections.Generic;
using View;

public class Fsm : IUpdateble
{
    private FsmState _curentState;
    private Dictionary<Type, FsmState> _states = new();

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if (_curentState != null && _curentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var nextState))
        {
            _curentState?.Exit();
            _curentState = nextState;
            _curentState.Enter();
        }
        else
        {
            throw new InvalidOperationException();
        }

    }

    public Fsm BindState(FsmState state)
    {
        _states.TryAdd(state.GetType(), state);
        return this;
    }

    public void Update()
    => _curentState.Update();
}