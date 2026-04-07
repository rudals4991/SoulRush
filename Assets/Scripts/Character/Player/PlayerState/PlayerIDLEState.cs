using UnityEngine;

public class PlayerIDLEState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.IDLE;
    public PlayerIDLEState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Enter IDLE");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Debug.Log("Exit IDLE");
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.Move:
            case PlayerStateType.Guard:
            case PlayerStateType.Attack:
            case PlayerStateType.Roll:
            case PlayerStateType.Hit:
            case PlayerStateType.Dead:
            case PlayerStateType.Heal: return true;
            default: return false;
        }
    }
}
