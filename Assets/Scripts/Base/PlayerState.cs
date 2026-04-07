using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    public abstract PlayerStateType StateType { get; }

    protected PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
    public virtual bool CanTransitionTo(PlayerStateType nextState)
    { 
        return false;
    }
}
