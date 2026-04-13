using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Attack;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Attack");
        //player.Controller.Trigger("Attack");
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
