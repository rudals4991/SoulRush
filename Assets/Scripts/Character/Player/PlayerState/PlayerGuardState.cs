using UnityEngine;

public class PlayerGuardState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Guard;
    public PlayerGuardState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Guard");
        player.Movement.StopMove();
        player.Guard.StartGuard();
        player.Controller.Bool("IsGuard", true);
    }
    public override void Update()
    {
        player.Movement.StopMove();

        if (!player.InputReader.IsGuardPressed)
        {
            Debug.Log("Guard released -> Change to IDLE");
            stateMachine.ChangeState(player.IDLEState);
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Guard Exit");
        player.Guard.StopGuard();
        player.Controller.Bool("IsGuard", false);
        Debug.Log("Animator IsGuard = false");
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.IDLE:
            case PlayerStateType.Hit:
            case PlayerStateType.Dead: return true;

            default: return false;
        }
    }
}
