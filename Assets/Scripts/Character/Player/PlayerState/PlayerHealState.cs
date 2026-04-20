using UnityEngine;

public class PlayerHealState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Heal;
    bool hasHealAnim;
    bool hasAppliedHeal;
    public PlayerHealState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Heal");
        player.Movement.StopMove();
        hasHealAnim = false;
        hasAppliedHeal = false;
        if (player.PlayerHeal == null || !player.PlayerHeal.CanHeal())
        {
            stateMachine.ChangeState(player.IDLEState);
            return;
        }
        player.PlayerHeal.UseHeal();
        player.Controller.Trigger("Heal");
    }
    public override void Update()
    {
        if (player.Controller.IsCurrentStateName("Heal_Player"))
        {
            hasHealAnim = true;
            AnimatorStateInfo stateInfo = player.Controller.GetCurrentStateInfo();
            float normalizedTime = stateInfo.normalizedTime;
            if (!hasAppliedHeal && normalizedTime >= 0.5f)
            {
                ApplyHeal();
            }
            if (normalizedTime >= 0.95f)
            {
                ExitState();
                return;
            }
        }
        else if (hasHealAnim)
        {
            ExitState();
            return;
        }
    }
    void ApplyHeal()
    {
        if (hasAppliedHeal) return;
        hasAppliedHeal = true;
        player.Heal(player.PlayerHeal.HealAmount);
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
            case PlayerStateType.Hit:
            case PlayerStateType.Dead:
            case PlayerStateType.IDLE:
            case PlayerStateType.Move: return true;

            default: return false;
        }
    }
}
