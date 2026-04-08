using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Dead;
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Dead");
        //player.Controller.Trigger("Dead");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        return false;
    }
}
