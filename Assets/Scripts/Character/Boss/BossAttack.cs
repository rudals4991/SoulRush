using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Boss boss;
    public void Initialize(Boss owner)
    { 
        boss = owner;
    }
    public void ExecuteAttack()
    {
        if (boss == null) return;
        boss.Movement?.Stop();
        boss.MarkAttackTime();
        // 공격 애니메이션 실행
        // 예: boss.Animator.SetTrigger("Attack");
        // 실제 데미지 판정은 Animation Event에서 히트박스 On/Off 처리
    }
}
