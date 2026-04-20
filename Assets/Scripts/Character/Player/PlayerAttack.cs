using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    AttackHitBox attackHitBox;
    CharacterStat stat;
    PlayerStamina stamina;
    public bool IsAttacking { get; private set; }
    public int CurrentCombo { get; private set; }
    public bool IsNextCombo { get; private set; }
    public int maxCombo = 4;
    public void Initialize(Player player, AttackHitBox attackHitBox, CharacterStat stat, PlayerStamina stamina)
    {
        this.player = player;
        this.attackHitBox = attackHitBox;
        this.stat = stat;
        this.stamina = stamina;
    }
    public void StartFirstAttack()
    {
        IsAttacking = true;
        CurrentCombo = 1;
        IsNextCombo = false;
    }
    public void StartNextCombo()
    {
        if (CurrentCombo >= maxCombo) return;
        CurrentCombo++;
        IsNextCombo = false;
    }
    public void QueueNextCombo()
    {
        if (!IsAttacking) return;
        if (CurrentCombo >= maxCombo) return;
        IsNextCombo = true;
    }
    public bool CanNextCombo()
    {
        return IsAttacking && CurrentCombo < maxCombo;
    }
    public void EndAttack()
    {
        IsAttacking = false;
        CurrentCombo = 0;
        IsNextCombo = false;
        DisableHitBox();
    }
    public float GetAttackDamage(int combo)
    {
        if (stat == null) return 0f;
        return combo switch
        {
            1 => player.StatRuntime.GetAttack(),
            2 => player.StatRuntime.GetAttack() + 2f,
            3 => player.StatRuntime.GetAttack() + 4f,
            4 => player.StatRuntime.GetAttack() + 7f,
            _ => player.StatRuntime.GetAttack()
        };
    }
    public float GetCurrentAttackDamage()
    {
        return GetAttackDamage(CurrentCombo);
    }
    public float GetCurrentAttackStaminaCost()
    {
        return GetCurrentAttackDamage();
    }
    public bool CanAttack()
    {
        if (stamina == null) return false;
        return stamina.CanUse(GetCurrentAttackStaminaCost());
    }
    public bool TryUseAttackStamina()
    {
        if (stamina == null) return false;
        return stamina.TryUse(GetCurrentAttackStaminaCost(), true);
    }
    public bool TryUseAttackStamina(int combo)
    {
        if (stamina == null) return false;
        return stamina.TryUse(GetAttackDamage(combo), true);
    }
    public void EnableHitBox()
    {
        if (attackHitBox == null) return;
        attackHitBox.SetDamage(GetCurrentAttackDamage());
        attackHitBox.Activate();
    }
    public void DisableHitBox()
    {
        if (attackHitBox == null) return;
        attackHitBox.Deactivate();
    }
}
