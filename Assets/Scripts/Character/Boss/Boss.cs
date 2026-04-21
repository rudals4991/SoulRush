using UnityEngine;

public class Boss : CharacterBase
{
    [SerializeField]float detectRange = 20f;
    protected BossStat bossStat;

    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    public BossMovement Movement { get; private set; }
    public BossAttack BossAttack { get; private set; }

    protected bool hasTarget;
    protected bool isPhase2;
    protected bool isGroggy;
    protected bool isPhaseChange;

    protected bool hasTriggeredFirstGroggy;
    protected bool hasTriggeredSecondGroggy;
    protected bool hasTriggeredPhase2;

    protected float lastAttackTime;
    protected float chaseStartTime;

    public float DetectRange => detectRange;
    public float AttackRange => bossStat.attackRange;
    public float AttackCooldown => bossStat.attackCooldown;
    public float GroggyDuration => bossStat.groggyDuration;
    public float StopDistance => bossStat.stopDistance;
    public float MaxChaseTime => bossStat.maxChaseTime;
    public BossStat BossStat => bossStat;
    protected void Awake()
    {
        bossStat = stat as BossStat;
    }
    public override void Initialize()
    {
        Rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Movement = GetComponent<BossMovement>();
        BossAttack = GetComponent<BossAttack>();
        Movement.Initialize(this, Rb);
        BossAttack.Initialize(this);
    }
    public bool HasTarget()
    {
        return target != null;
    }
    public bool IsTargetInDetectRange()
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= DetectRange;
    }
    public bool IsTargetInAttackRange()
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= AttackRange;
    }
    public bool IsTargetInStopDistance()
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= StopDistance;
    }
    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + AttackCooldown;
    }
    public float GetHpRatio()
    {
        return currentHp / bossStat.baseMaxHp;
    }
    public bool CanTriggerFirstGroggy()
    {
        if (hasTriggeredFirstGroggy) return false;
        return GetHpRatio() <= bossStat.firstGroggyPercent;
    }
    public bool CanTriggerPhase2()
    {
        if (hasTriggeredPhase2) return false;
        return GetHpRatio() <= bossStat.phase2Percent;
    }
    public bool CanTriggerSecondGroggy()
    {
        if (hasTriggeredSecondGroggy) return false;
        return GetHpRatio() <= bossStat.secondGroggyPercent;
    }
    public void TryFindTarget(Transform player)
    {
        if (target != null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= DetectRange) target = player;
    }
    public void EnterGroggy()
    {
        isGroggy = true;
        Movement.Stop();
        Invoke(nameof(ExitGroggy), GroggyDuration);
    }
    void ExitGroggy()
    {
        isGroggy = false;
    }
    public void EnterPhase2()
    {
        isPhaseChange = true;
        Movement.Stop();
        // Animator.SetTrigger("Phase2");
        Invoke(nameof(ExitPhaseChange), 2f);
    }
    private void ExitPhaseChange()
    {
        isPhaseChange = false;
        isPhase2 = true;
    }
    public void StartAttack()
    {
        lastAttackTime = Time.time;
        Movement.Stop();
        // Animator.Trigger("Attack");
    }
    public void UpdateState()
    {
        if (IsDead) return;
        // Phase2
        if (CanTriggerPhase2())
        {
            hasTriggeredPhase2 = true;
            EnterPhase2();
            return;
        }
        // Groggy 1
        if (CanTriggerFirstGroggy())
        {
            hasTriggeredFirstGroggy = true;
            EnterGroggy();
            return;
        }
        // Groggy 2
        if (CanTriggerSecondGroggy())
        {
            hasTriggeredSecondGroggy = true;
            EnterGroggy();
            return;
        }
    }
}
