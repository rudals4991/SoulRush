using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    float searchRadius = 10f;
    CharacterBase owner;
    LayerMask targetLayer;
    public CharacterBase CurrentTarget { get; private set; }
    public bool IsLockedOn => CurrentTarget != null;
    public Transform CurrentLockOnPoint => CurrentTarget != null ? CurrentTarget.LockOnPoint : null;
    public void Initialize(CharacterBase owner,LayerMask targetLayer)
    { 
        this.owner = owner;
        this.targetLayer = targetLayer;
    }
    public void ToggleLockOn()
    {
        if (IsLockedOn)
        {
            ClearTarget();
            return;
        }
        FindTarget();
    }
    public void ClearTarget()
    {
        CurrentTarget = null;
    }
    public void ValidateTarget()
    {
        if (CurrentTarget == null) return;
        if (!CurrentTarget.gameObject.activeInHierarchy || !CurrentTarget.CanBeLockedOn())
        {
            ClearTarget();
        }
    }
    private void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, searchRadius, targetLayer);

        if (hits == null || hits.Length == 0) return;
        CharacterBase nearestTarget = null;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            CharacterBase candidate = hits[i].GetComponentInParent<CharacterBase>();
            if (candidate == null)  continue;
            if (candidate == owner) continue;
            if (!candidate.CanBeLockedOn()) continue;

            float sqrDistance = (candidate.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < nearestDistance)
            {
                nearestDistance = sqrDistance;
                nearestTarget = candidate;
            }
        }
        if (nearestTarget == null) return;
        CurrentTarget = nearestTarget;
    }
}
