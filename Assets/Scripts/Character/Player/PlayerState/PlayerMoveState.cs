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
        Vector2 moveInput = player.InputReader.MoveInput;
        if (moveInput != Vector2.zero)
        {
            //가드 이동 처리
        }
    }
    public override void Exit()
    {

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
