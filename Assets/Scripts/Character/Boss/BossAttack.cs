using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Boss boss;
    [SerializeField] int attackCount = 6;
    public void Initialize(Boss owner)
    { 
        boss = owner;
    }
    public void ExecuteAttack()
    {
        if (boss == null) return;
        boss.EnterAttackState();
        int attackIndex = Random.Range(0, attackCount);
        boss.AnimController?.Int("AttackIndex", attackIndex);
        boss.AnimController?.Trigger("Attack");
    }
    public void FinishAttack()
    {
        if (boss == null) return;
        boss.ExitAttackState();
    }
}
