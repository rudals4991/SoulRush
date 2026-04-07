using UnityEngine;

public class PlayerGuardState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Guard;
    public PlayerGuardState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
