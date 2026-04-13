using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Move;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Move Enter");
    }
    public override void Update()
    {
        Vector2 moveInput = player.InputReader.MoveInput;
        if (moveInput.sqrMagnitude <= 0.01f)
        {
            player.Movement.StopMove();
            stateMachine.ChangeState(player.IDLEState); 
            return;
        }
        bool isLockedOn = player.LockOn != null && player.LockOn.IsLockedOn;
        Transform targetPoint = isLockedOn ? player.LockOn.CurrentLockOnPoint : null;
        if (isLockedOn)
        {
            player.Movement.SetMoveInput(moveInput, false, true, targetPoint);
            return;
        }
        bool isRunning = player.InputReader.IsRunPressed;
        player.Movement.SetMoveInput(moveInput, isRunning, false, null);
    }
    public override void Exit()
    {
        player.Movement.StopMove();
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.IDLE:
            case PlayerStateType.Guard:
            case PlayerStateType.Attack:
            case PlayerStateType.Roll:
            case PlayerStateType.Hit:
            case PlayerStateType.Heal:
            case PlayerStateType.Dead: return true;

            default: return false;
        }
    }
}
