using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    CharacterStat stat;
    public float CurrentStamina { get; private set; }
    public float MaxStamina => stat.baseMaxStamina;
    public bool IsGuardBroken { get; private set; }
    float regenDelayTimer;

    public void Initialize(CharacterStat stat)
    {
        this.stat = stat;
        CurrentStamina = stat.baseMaxStamina;
        IsGuardBroken = false;
        regenDelayTimer = 0f;
    }
    public void Tick(float deltaTime)
    {
        if (stat == null) return;
        if (regenDelayTimer > 0f)
        {
            regenDelayTimer -= deltaTime;
            if (regenDelayTimer < 0f) regenDelayTimer = 0f;
            return;
        }
        Regenerate(deltaTime);
    }
    private void Regenerate(float deltaTime)
    {
        if (CurrentStamina >= MaxStamina) return;
        CurrentStamina += stat.staminaRegen * deltaTime;
        CurrentStamina = Mathf.Min(CurrentStamina, MaxStamina);
        if (IsGuardBroken && CurrentStamina >= MaxStamina * 0.3f) IsGuardBroken = false;
    }
    public bool CanGuard()
    {
        return CurrentStamina > 0f && IsGuardBroken == false;
    }
    public bool CanUse(float cost)
    {
        return CurrentStamina >= cost;
    }
    public bool TryUse(float cost, bool applyRegenDelay = true)
    {
        if (CurrentStamina < cost) return false;
        CurrentStamina -= cost;
        CurrentStamina = Mathf.Max(CurrentStamina, 0f);
        if (applyRegenDelay) StartRegenDelay(0.8f);
        return true;
    }
    public void ForceUse(float cost, bool applyRegenDelay = true)
    {
        CurrentStamina -= cost;
        CurrentStamina = Mathf.Max(CurrentStamina, 0f);
        if (applyRegenDelay) StartRegenDelay(0.8f);
    }
    public float ApplyGuardStaminaDamage(float incomingDamage)
    {
        float staminaDamage = incomingDamage * 0.5f;

        CurrentStamina -= staminaDamage;
        CurrentStamina = Mathf.Max(CurrentStamina, 0f);
        StartRegenDelay(0.8f);
        if (CurrentStamina <= 0f) IsGuardBroken = true;
        return staminaDamage;
    }
    public void StartRegenDelay(float delay)
    {
        regenDelayTimer = delay;
    }
    public void RecoverFull()
    {
        CurrentStamina = MaxStamina;
        IsGuardBroken = false;
        regenDelayTimer = 0f;
    }
}
