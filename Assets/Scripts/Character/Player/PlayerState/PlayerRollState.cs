using UnityEngine;

public class PlayerRollState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Roll;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Roll");
        //player.Controller.Trigger("Roll");
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
            case PlayerStateType.Hit:
            case PlayerStateType.Dead:
            case PlayerStateType.IDLE: return true;

            default: return false;
        }
    }
}
