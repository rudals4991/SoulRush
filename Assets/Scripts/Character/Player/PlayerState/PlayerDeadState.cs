using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Dead;
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
