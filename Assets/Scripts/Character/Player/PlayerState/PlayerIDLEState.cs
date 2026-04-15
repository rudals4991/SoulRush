using UnityEngine;

public class PlayerIDLEState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.IDLE;
    public PlayerIDLEState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Enter IDLE");
        player.Movement.StopMove();
        player.Controller.Float("Speed", 0f);
    }
    public override void Update()
    {
        if (player.InputReader.AttackPressed)
        {
            stateMachine.ChangeState(player.AttackState);
            return;
        }
        if (player.InputReader.RollPressed)
        {
            stateMachine.ChangeState(player.RollState);
            return;
        }
        if (player.InputReader.HealPressed)
        {
            stateMachine.ChangeState(player.HealState);
            return;
        }
        if (player.InputReader.IsGuardPressed)
        {
            stateMachine.ChangeState(player.GuardState);
            return;
        }
        if (player.InputReader.MoveInput.sqrMagnitude > 0.01f)
        {
            stateMachine.ChangeState(player.MoveState);
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Exit IDLE");
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.Move:
            case PlayerStateType.Guard:
            case PlayerStateType.Attack:
            case PlayerStateType.Roll:
            case PlayerStateType.Hit:
            case PlayerStateType.Dead:
            case PlayerStateType.Heal: return true;
            default: return false;
        }
    }
}
