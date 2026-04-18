using UnityEngine;

public class PlayerRollState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Roll;
    bool hasRollAnim;
    public PlayerRollState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Roll");
        player.Movement.StopMove();
        Vector3 rollDirection = GetRollDirection();
        player.Roll.StartRoll(rollDirection);
        hasRollAnim = false;
        player.Controller.Trigger("Roll");
    }
    public override void Update()
    {
        player.Roll.Tick(Time.deltaTime);
        if (player.Controller.IsCurrentStateName("Roll_Player"))
        {
            hasRollAnim = true;
            AnimatorStateInfo stateInfo = player.Controller.GetCurrentStateInfo();
            float normalizedTime = stateInfo.normalizedTime;
            bool isInvincibleWindow = normalizedTime >= 0.10f && normalizedTime <= 0.6f;
            player.Roll.SetInvincible(isInvincibleWindow);
            if (normalizedTime >= 0.95f)
            {
                ExitRollState();
                return;
            }
        }
        else if (hasRollAnim)
        {
            ExitRollState();
            return;
        }
    }
    void ExitRollState()
    {
        if (player.InputReader.MoveInput.sqrMagnitude > 0.01f)
            stateMachine.ChangeState(player.MoveState);
        else
            stateMachine.ChangeState(player.IDLEState);
    }
    public override void Exit()
    {
        player.Roll.EndRoll();
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.Hit:
            case PlayerStateType.Dead:
            case PlayerStateType.IDLE:
            case PlayerStateType.Move: return true;
            default: return false;
        }
    }
    Vector3 GetRollDirection()
    {
        Vector2 moveInput = player.InputReader.MoveInput;
        if (moveInput.sqrMagnitude > 0.01f)
        {
            return GetCamDirection(moveInput);
        }
        return player.transform.forward;
    }
    Vector3 GetCamDirection(Vector2 input)
    {
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 direction = forward * input.y + right * input.x;
        return direction.normalized;
    }
}
