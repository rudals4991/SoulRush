using UnityEngine;

public class PlayerGuardState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Guard;
    public PlayerGuardState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Guard");
        //player.Controller.Trigger("GuardStart");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.IDLE:
            case PlayerStateType.Roll:
            case PlayerStateType.Hit:
            case PlayerStateType.Dead: return true;

            default: return false;
        }
    }
}
