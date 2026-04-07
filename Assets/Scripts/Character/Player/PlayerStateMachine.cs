using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


//State : IDLE. Move(Run, Walk), Guard, Attack, Roll, Hit, Dead
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public void Initialize(PlayerState firstState)
    { 
        CurrentState = firstState;
        CurrentState.Enter();
    }
    public bool ChangeState(PlayerState newState)
    {
        if (newState == null) return false;
        if (CurrentState == null)
        {
            CurrentState = newState;
            CurrentState.Enter();
            return true;
        }
        if (!CurrentState.CanTransitionTo(newState.StateType)) return false;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
        return true;
    }
    public void Update()
    {
        CurrentState?.Update();
    }
}
