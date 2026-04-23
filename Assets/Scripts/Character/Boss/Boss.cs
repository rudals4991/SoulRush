using UnityEngine;

public class Boss : CharacterBase
{
    [SerializeField] private float detectRange = 20f;
    [SerializeField] private float phaseChangeDuration = 2f;

    protected BossStat bossStat;

    protected float lastAttackTime;
    protected float chaseStartTime;
    protected float groggyStartTime;
    protected float phaseChangeStartTime;

    protected bool isPhase2;
    protected bool isGroggy;
    protected bool isPhaseChange;

    protected bool hasTriggeredFirstGroggy;
    protected bool hasTriggeredSecondGroggy;
    protected bool hasTriggeredPhase2;
    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    public BossBT BT { get; private set; }
    public BossMovement Movement { get; private set; }
    public BossAttack BossAttack { get; private set; }

    public float DetectRange => detectRange;
    public float AttackRange => bossStat.attackRange;
    public float AttackCooldown => bossStat.attackCooldown;
    public float GroggyDuration => bossStat.groggyDuration;
    public float StopDistance => bossStat.stopDistance;
    public float MaxChaseTime => bossStat.maxChaseTime;

    public BossStat BossStat => bossStat;

    public bool IsGroggy => isGroggy;
    public bool IsPhaseChanging => isPhaseChange;
    public bool IsPhase2 => isPhase2;

    protected void Awake()
    {
        bossStat = stat as BossStat;
        Initialize();
    }
    public override void Initialize()
    {
        Rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Movement = GetComponent<BossMovement>();
        BossAttack = GetComponent<BossAttack>();
        BT = GetComponent<BossBT>();
        Movement.Initialize(this, Rb);
        BossAttack.Initialize(this);
        BT.Initialize(this);
    }
    public bool HasTarget()
    {
        return target != null;
    }
    public void TryFindTarget(Transform player)
    {
        if (target != null || player == null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= DetectRange) target = player;
    }
    public bool IsTargetInDetectRange()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= DetectRange;
    }
    public bool IsTargetInAttackRange()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= AttackRange;
    }
    public bool IsTargetInStopDistance()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= StopDistance;
    }
    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + AttackCooldown;
    }
    public void MarkAttackTime()
    {
        lastAttackTime = Time.time;
    }
    public float GetHpRatio()
    {
        if (bossStat == null || bossStat.baseMaxHp <= 0f) return 0f;
        return currentHp / bossStat.baseMaxHp;
    }
    public bool CanTriggerFirstGroggy()
    {
        if (hasTriggeredFirstGroggy) return false;
        return GetHpRatio() <= bossStat.firstGroggyPercent;
    }
    public bool CanTriggerSecondGroggy()
    {
        if (hasTriggeredSecondGroggy) return false;
        return GetHpRatio() <= bossStat.secondGroggyPercent;
    }
    public bool CanTriggerPhase2()
    {
        if (hasTriggeredPhase2) return false;
        return GetHpRatio() <= bossStat.phase2Percent;
    }
    public void SetFirstGroggyTriggered()
    {
        hasTriggeredFirstGroggy = true;
    }
    public void SetSecondGroggyTriggered()
    {
        hasTriggeredSecondGroggy = true;
    }
    public void SetPhase2Triggered()
    {
        hasTriggeredPhase2 = true;
    }
    public void EnterGroggyState()
    {
        isGroggy = true;
        groggyStartTime = Time.time;
        Movement?.Stop();
    }
    public void ExitGroggyState()
    {
        isGroggy = false;
        groggyStartTime = 0f;
    }
    public bool IsGroggyFinished()
    {
        if (!isGroggy) return true;
        return Time.time >= groggyStartTime + GroggyDuration;
    }
    public void EnterPhaseChangeState()
    {
        isPhaseChange = true;
        phaseChangeStartTime = Time.time;
        Movement?.Stop();
    }
    public void ExitPhaseChangeState()
    {
        isPhaseChange = false;
        isPhase2 = true;
        phaseChangeStartTime = 0f;
    }
    public bool IsPhaseChangeFinished()
    {
        if (!isPhaseChange) return true;
        return Time.time >= phaseChangeStartTime + phaseChangeDuration;
    }
    public void StartChase()
    {
        if (chaseStartTime <= 0f) chaseStartTime = Time.time;
    }
    public void ResetChase()
    {
        chaseStartTime = 0f;
    }
    public bool HasExceededMaxChaseTime()
    {
        if (chaseStartTime <= 0f) return false;
        return Time.time >= chaseStartTime + MaxChaseTime;
    }
}
