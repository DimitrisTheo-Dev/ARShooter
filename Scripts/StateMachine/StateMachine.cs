using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public StateMachine()
    {
        Debug.Log("<<<<<<<< StateMachine Created >>>>>>>>");
    }

    public void ChangeState(IState state)
    {
        _currentState?.ExitState();
        _currentState = state;
        state.EnterState();
    }

    public void ExecuteStateUpdate()
    {
        _currentState?.ExecuteState();
    }
}