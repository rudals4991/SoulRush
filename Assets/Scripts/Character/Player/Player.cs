using UnityEngine;

public class Player : CharacterBase
{
    public CharacterStatRuntime StatRuntime { get; private set; }
    Animator animator;
    Rigidbody rb;
    Camera cam; //추후 주입
    [SerializeField] AttackHitBox attackHitBox;
    #region Player Components
    public PlayerAnimController Controller { get; private set; }
    public PlayerInputReader InputReader { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerGuard Guard { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerRoll Roll { get; private set; }
    public PlayerHeal PlayerHeal { get; private set; }
    public PlayerLockOn LockOn { get; private set; }
    public PlayerStamina Stamina { get; private set; }
    #endregion

    #region Player States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIDLEState IDLEState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerHealState HealState { get; private set; }
    #endregion

    public void Start()
    {
        Initialize();
    }
    public override void Initialize()
    {
        StatRuntime = GetComponent<CharacterStatRuntime>();
        StatRuntime.Initialize(stat);
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        Controller = GetComponent<PlayerAnimController>();
        InputReader = GetComponent<PlayerInputReader>();
        Movement = GetComponent<PlayerMovement>();
        LockOn = GetComponent<PlayerLockOn>();
        Guard = GetComponent<PlayerGuard>();
        PlayerAttack = GetComponent<PlayerAttack>();
        Roll = GetComponent<PlayerRoll>();
        PlayerHeal = GetComponent<PlayerHeal>();
        Stamina = GetComponent<PlayerStamina>();

        ResetStat();

        StateMachine = new PlayerStateMachine();

        IDLEState = new PlayerIDLEState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        GuardState = new PlayerGuardState(this, StateMachine);
        AttackState = new PlayerAttackState(this, StateMachine);
        RollState = new PlayerRollState(this, StateMachine);
        HitState = new PlayerHitState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);
        HealState = new PlayerHealState(this, StateMachine);

        Controller.Initialize(animator);
        Movement.Initialize(rb, cam.transform, stat.baseMoveSpeed, 12f);
        LockOn.Initialize(this, LayerMask.GetMask("Monster"));
        Guard.Initialize(this, stat, Stamina);
        PlayerAttack.Initialize(this, attackHitBox, stat, Stamina);
        Roll.Initialize(rb, 1.4f, Stamina);
        attackHitBox?.Initialize(this);

        StateMachine.Initialize(IDLEState);
    }
    public override float MaxHp => StatRuntime != null ? StatRuntime.GetMaxHp() : base.MaxHp;
    public override float CurrentHp => StatRuntime != null ? StatRuntime.CurrentHp : base.CurrentHp;

    private void Update()
    {
        if (StateMachine == null) return;
        ControlInput();
        LockOn?.ValidateTarget();
        Stamina?.Tick(Time.deltaTime);
        StateMachine.Update();
    }
    private void FixedUpdate()
    {
        if (StateMachine.CurrentState.StateType == PlayerStateType.Roll ||
            StateMachine.CurrentState.StateType == PlayerStateType.Dead) return;
        Movement?.FixedTick();
    }
    private void ControlInput()
    {
        if (StateMachine.CurrentState == null) return;
        if (StateMachine.CurrentState.StateType == PlayerStateType.Dead) return;
        if (InputReader.LockOnPressed)
        {
            LockOn?.ToggleLockOn();
        }
    }
    public void OnGuardHit()
    {
        if (!Guard.IsGuarding) return;
        Controller.Trigger("GuardHit");
    }
    public override void TakeDamage(float damage, CharacterBase attacker)
    {
        if (StateMachine.CurrentState.StateType == PlayerStateType.Dead) return;
        if (Roll.IsInvincible) return;
        if (Guard.IsGuarding)
        {
            float guardedDamage = Guard.ApplyGuard(damage);
            OnGuardHit();
            StatRuntime.TakeDamage(guardedDamage);
            if (StatRuntime.CurrentHp <= 0f)
            {
                Die();
                StateMachine.ChangeState(DeadState);
                return;
            }
            if (Guard.IsBroken()) StateMachine.ChangeState(HitState);
            return;
        }
        StatRuntime.TakeDamage(damage);
        if (StatRuntime.CurrentHp <= 0f)
        {
            Die();
            StateMachine.ChangeState(DeadState);
            return;
        }
        StateMachine.ChangeState(HitState);
    }
    public override void Heal(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;
        StatRuntime.Heal(amount);
    }
    public void EnableAttackHitBox()
    {
        PlayerAttack?.EnableHitBox();
    }
    public void DisableAttackHitBox()
    {
        PlayerAttack?.DisableHitBox();
    }
    public override void ResetStat()
    {
        base.ResetStat();
        StatRuntime.Initialize(stat);
        Stamina.Initialize(stat);
        IsDead = false;
        IsInvincible = false;
    }
}
