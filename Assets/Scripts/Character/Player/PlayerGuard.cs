using UnityEngine;

public class PlayerGuard : MonoBehaviour
{
    Player player;
    CharacterStat stat;
    PlayerStamina stamina;
    public bool IsGuarding { get; private set; }
    public void Initialize(Player player, CharacterStat stat, PlayerStamina stamina)
    {
        this.player = player;
        this.stat = stat;
        this.stamina = stamina;
    }
    public bool CanStartGuard()
    {
        if (stamina == null) return false;
        return stamina.CanGuard();
    }
    public void StartGuard()
    {
        IsGuarding = true;
    }
    public void StopGuard()
    {
        IsGuarding = false;
    }
    public float GetGuardedDamage(float incomingDamage)
    {
        if (stat == null) return incomingDamage;

        float guardPercent = Mathf.Clamp(player.StatRuntime.GetGuardPercent(), 0f, 100f);
        float reducedDamage = incomingDamage * (1f - guardPercent / 100f);
        return Mathf.Max(reducedDamage, 0f);
    }
    public float ApplyGuard(float incomingDamage)
    {
        if (stamina == null) return incomingDamage;
        stamina.ApplyGuardStaminaDamage(incomingDamage);
        return GetGuardedDamage(incomingDamage);
    }
    public bool IsBroken()
    {
        if (stamina == null) return false;
        return stamina.IsGuardBroken;
    }
}
