using UnityEngine;

public class PlayerHealState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Heal;
    public PlayerHealState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
