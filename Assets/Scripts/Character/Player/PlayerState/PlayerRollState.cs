using UnityEngine;

public class PlayerRollState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Roll;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
