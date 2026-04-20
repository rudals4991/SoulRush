using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackHitBox : MonoBehaviour
{
    readonly HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();
    CharacterBase owner;
    float damage;
    bool isActive;
    Collider hitCollider;

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
        hitCollider.isTrigger = true;
        hitCollider.enabled = true;
        isActive = false;
    }
    public void Initialize(CharacterBase owner)
    {
        this.owner = owner;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void Activate()
    {
        isActive = true;
        hitTargets.Clear();
    }
    public void Deactivate()
    {
        isActive = false;
        hitTargets.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (owner != null && other.transform.root == owner.transform) return;
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;
        if (hitTargets.Contains(damageable)) return;
        hitTargets.Add(damageable);
        damageable.TakeDamage(damage, owner);
    }
}
