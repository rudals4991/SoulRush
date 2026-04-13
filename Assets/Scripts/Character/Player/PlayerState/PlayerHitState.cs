using UnityEngine;

public class PlayerHitState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Hit;
    public PlayerHitState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Hit");
        //player.Controller.Trigger("Hit");
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
            case PlayerStateType.Dead:
            case PlayerStateType.IDLE: return true;

            default: return false;
        }
    }
}
