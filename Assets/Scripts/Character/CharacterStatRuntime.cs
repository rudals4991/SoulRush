using UnityEngine;

public class CharacterStatRuntime : MonoBehaviour
{
    CharacterStat baseStat;
    float bonusAttack;
    float bonusGuard;
    float bonusHp;
    public float CurrentHp { get; private set; }
    public void Initialize(CharacterStat stat)
    {
        baseStat = stat;
        bonusAttack = 0f;
        bonusGuard = 0f;
        bonusHp = 0f;
        CurrentHp = GetMaxHp();
    }
    public float GetAttack()
    {
        return baseStat.baseAttackDamage + bonusAttack;
    }
    public float GetGuardPercent()
    {
        return Mathf.Clamp(baseStat.baseGuardPercent + bonusGuard, 0f, 100f);
    }
    public float GetMaxHp()
    {
        return baseStat.baseMaxHp + bonusHp;
    }
    public void AddAttack(float amount)
    {
        bonusAttack += amount;
    }
    public void AddGuardPercent(float amount)
    {
        bonusGuard += amount;
    }
    public void AddMaxHp(float amount)
    {
        bonusHp += amount;
        CurrentHp += amount;
        CurrentHp = Mathf.Min(CurrentHp, GetMaxHp());
    }
    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0f) CurrentHp = 0f;
    }
    public void Heal(float amount)
    {
        CurrentHp += amount;
        if (CurrentHp > GetMaxHp()) CurrentHp = GetMaxHp();
    }
}
