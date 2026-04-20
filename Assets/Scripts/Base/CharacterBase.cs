using UnityEngine;

public abstract class CharacterBase : MonoBehaviour,IDamageable
{
    [SerializeField] protected CharacterStat stat;
    [SerializeField] protected Transform lockOnPoint;
    
    protected float currentHp;
    protected float currentStamina;

    public bool IsDead { get; protected set; }
    public bool IsInvincible { get; protected set; }

    protected Transform target;

    public virtual float MaxHp => stat.baseMaxHp;
    public virtual float CurrentHp => currentHp;
    public virtual float MaxStamina => stat.baseMaxStamina;
    public virtual float CurrentStamina => currentStamina;
    public Transform Target => target;
    public Transform LockOnPoint
    {
        get
        {
            if (lockOnPoint != null)
                return lockOnPoint;

            return transform;
        }
    }
    public virtual bool CanBeLockedOn()
    {
        return gameObject.activeInHierarchy;
    }
    public virtual void Initialize()
    {
        ResetStat();
    }
    public virtual void ResetStat()
    {
        currentHp = stat.baseMaxHp;
        currentStamina = stat.baseMaxStamina;
        IsDead = false;
        IsInvincible = false;
        target = null;
    }
    public virtual void TakeDamage(float damage, CharacterBase attacker)
    {
        if (IsDead || IsInvincible) return;
        currentHp -= damage;
        if (currentHp <= 0) Die();
    }
    public virtual void Heal(float amount)
    {
        if (IsDead) return;
        if (amount <= 0) return;
        currentHp = Mathf.Min(currentHp + amount, MaxHp);
    }
    public virtual void Die()
    {
        if (IsDead) return;
        IsDead = true;
    }
    public virtual void Attack() { }
    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }
}
