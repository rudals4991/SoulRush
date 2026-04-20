using UnityEngine;

public class PlayerHitState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Hit;
    bool hasHitAnim;
    public PlayerHitState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Hit");
        player.Movement.StopMove();
        hasHitAnim = false;
        player.Controller.Trigger("Hit");
    }
    public override void Update()
    {
        if (player.Controller.IsCurrentStateName("Hit_Player"))
        {
            hasHitAnim = true;
            AnimatorStateInfo stateInfo = player.Controller.GetCurrentStateInfo();
            float normalizedTime = stateInfo.normalizedTime;
            if (normalizedTime >= 0.95f)
            {
                ExitState();
                return;
            }
        }
        else if (hasHitAnim)
        {
            ExitState();
            return;
        }
    }
    void ExitState()
    {
        if (player.InputReader.MoveInput.sqrMagnitude > 0.01f) stateMachine.ChangeState(player.MoveState);
        else stateMachine.ChangeState(player.IDLEState);
    }
    public override void Exit()
    {
        
    }
    public override bool CanTransitionTo(PlayerStateType nextState)
    {
        switch (nextState)
        {
            case PlayerStateType.Dead:
            case PlayerStateType.IDLE:
            case PlayerStateType.Move: return true;

            default: return false;
        }
    }
}
