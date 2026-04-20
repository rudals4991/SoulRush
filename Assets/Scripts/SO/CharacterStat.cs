using UnityEngine;

[CreateAssetMenu(menuName = "Game/CharacterStat")]
public class CharacterStat : ScriptableObject
{
    [Header("캐릭터 체력")]
    public float baseMaxHp;

    [Header("캐릭터 이동속도")]
    public float baseMoveSpeed;

    [Header("캐릭터 스테미나")]
    public float baseMaxStamina;
    public float staminaRegen;

    [Header("캐릭터 기본 공격 데미지")]
    public float baseAttackDamage;

    [Header("캐릭터 기본 가드 감소율")]
    public float baseGuardPercent;
}
