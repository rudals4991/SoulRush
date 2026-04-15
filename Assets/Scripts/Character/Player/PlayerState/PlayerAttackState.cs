using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public override PlayerStateType StateType => PlayerStateType.Attack;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Attack");
        player.Movement.StopMove();
        player.Controller.ResetAttackTriggers();
        player.PlayerAttack.StartFirstAttack();
        PlayCurrentAttack();
    }
    public override void Update()
    {
        HandleComboInput();

        string currentAttackState = GetCurrentAttackStateName();
        if (string.IsNullOrEmpty(currentAttackState)) return;

        // 현재 재생 중인 애니메이션 정보 가져오기
        var stateInfo = player.Controller.GetCurrentStateInfo();

        if (stateInfo.IsName(currentAttackState))
        {
            if (stateInfo.normalizedTime >= 0.8f)
            {
                if (player.PlayerAttack.IsNextCombo)
                {
                    TryNextComboOrEnd();
                }
                else if (stateInfo.normalizedTime >= 0.95f)
                {
                    stateMachine.ChangeState(player.IDLEState);
                }
            }
        }
        else if (!player.Controller.IsInTransition())
        {
            stateMachine.ChangeState(player.IDLEState);
        }
    }
    public override void Exit()
    {
        player.Controller.ResetAttackTriggers();
        player.PlayerAttack.EndAttack();
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
    void TryNextComboOrEnd()
    {
        if (player.PlayerAttack.IsNextCombo && player.PlayerAttack.CurrentCombo < player.PlayerAttack.maxCombo)
        {
            player.PlayerAttack.StartNextCombo();
            PlayCurrentAttack();
            return;
        }
        stateMachine.ChangeState(player.IDLEState);
    }
    void HandleComboInput()
    {
        if (!player.InputReader.AttackPressed) return;
        player.PlayerAttack.QueueNextCombo();
    }
    void PlayCurrentAttack()
    {
        switch (player.PlayerAttack.CurrentCombo)
        {
            case 1: player.Controller.Trigger("Attack1"); break;
            case 2: player.Controller.Trigger("Attack2"); break;
            case 3: player.Controller.Trigger("Attack3"); break;
            case 4: player.Controller.Trigger("Attack4"); break;
        }
    }
    string GetCurrentAttackStateName()
    {
        switch (player.PlayerAttack.CurrentCombo)
        {
            case 1: return "Attack1_Player";
            case 2: return "Attack2_Player";
            case 3: return "Attack3_Player";
            case 4: return "Attack4_Player";
            default: return string.Empty;
        }
    }
}
