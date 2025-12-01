using UnityEngine;

public class FiniteStateMachine<T> where T : IControllable
{
    private State<T> _currentState;

    public void StateInit(State<T> state, T controller)
    {
        _currentState = state;
        _currentState.Enter(controller);
    }

    public void UpdateState(T controller)
    {
        State<T> newState = _currentState.HandleTransitions(controller);

        if (newState != null)
        {
            ChangeState(newState, controller);
        }
        else
        {
            _currentState.Update(controller);
        }

        Debug.Log(_currentState);
    }

    public void LateUpdateState(T controller)
    {
        _currentState.LateUpdate(controller);
    }

    public void FixedUpdateState(T controller)
    {
        _currentState.FixedUpdate(controller);
    }

    private void ChangeState(State<T> state, T controller)
    { 
        if (state != null && state != _currentState)
        {
            _currentState.Exit(controller);
            _currentState = state;
            _currentState.Enter(controller);
        }
    }
}
