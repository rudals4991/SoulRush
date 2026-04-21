using UnityEngine;

[CreateAssetMenu(menuName = "Game/BossStat")]
public class BossStat : CharacterStat
{
    [Header("공격 쿨타임")]
    public float attackCooldown;
    [Header("공격 가능 범위")]
    public float attackRange;
    [Header("그로기 유지 시간")]
    public float groggyDuration;

    [Header("최대 추적 거리")]
    public float stopDistance;
    [Header("최대 추적 시간")]
    public float maxChaseTime;

    [Header("페이즈 관련")]
    [Range(0f, 1f)] public float firstGroggyPercent;
    [Range(0f, 1f)] public float secondGroggyPercent;
    [Range(0f, 1f)] public float phase2Percent;
}
