using UnityEngine;

public class PlayerHitState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Hit;
    public PlayerHitState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
