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

        Vector2 moveInput = player.InputReader.MoveInput;

        if (moveInput.sqrMagnitude <= 0.01f)
        {
            player.Movement.StopMove();
            player.Controller.Float("Speed", 0f);
            stateMachine.ChangeState(player.IDLEState);
            return;
        }

        bool isLockedOn = player.LockOn != null && player.LockOn.IsLockedOn;
        Transform targetPoint = isLockedOn ? player.LockOn.CurrentLockOnPoint : null;
        bool isRunning = !isLockedOn && player.InputReader.IsRunPressed;

        PlayerMovement.RotationMode rotationMode = isLockedOn
            ? PlayerMovement.RotationMode.LockOnTarget
            : PlayerMovement.RotationMode.MoveDirection;

        player.Movement.SetMoveInput(moveInput, isRunning, rotationMode, targetPoint);

        float speed = isRunning ? 1f : 0.5f;
        player.Controller.Float("Speed", speed);
    }
    public override void Exit()
    {
        player.Movement.StopMove();
        player.Controller.Float("Speed", 0f);
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
            case PlayerStateType.Dead:
            case PlayerStateType.Heal: return true;

            default:
                return false;
        }
    }
}
