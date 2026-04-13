using UnityEngine;

public class PlayerHealState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Heal;
    public PlayerHealState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Heal");
        //player.Controller.Trigger("Heal");
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
