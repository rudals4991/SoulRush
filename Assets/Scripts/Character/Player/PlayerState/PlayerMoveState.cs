using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Move;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
