using UnityEngine;

public class Player : CharacterBase
{
    Animator animator;
    Rigidbody rb;
    Camera cam; //추후 주입
    public PlayerAnimController Controller { get; private set; }
    public PlayerInputReader InputReader { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerGuard Guard { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerRoll Roll { get; private set; }
    public PlayerLockOn LockOn { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIDLEState IDLEState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerHealState HealState { get; private set; }

    public void Start()
    {
        Initialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        Controller = GetComponent<PlayerAnimController>();
        InputReader = GetComponent<PlayerInputReader>();
        Movement = GetComponent<PlayerMovement>();
        LockOn = GetComponent<PlayerLockOn>();
        //Guard = GetComponent<PlayerGuard>();
        //PlayerAttack = GetComponent<PlayerAttack>();
        //Roll = GetComponent<PlayerRoll>();

        StateMachine = new PlayerStateMachine();

        IDLEState = new PlayerIDLEState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        GuardState = new PlayerGuardState(this, StateMachine);
        AttackState = new PlayerAttackState(this, StateMachine);
        RollState = new PlayerRollState(this, StateMachine);
        HitState = new PlayerHitState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);
        HealState = new PlayerHealState(this, StateMachine);

        StateMachine.Initialize(IDLEState);

        Controller.Initialize(animator);
        Movement.Initialize(rb, cam.transform, stat.baseMoveSpeed, 12f);
        LockOn.Initialize(this,LayerMask.GetMask("Monster"));
    }

    private void Update()
    {
        if (StateMachine == null) return;
        ControlInput();
        LockOn?.ValidateTarget();
        StateMachine.Update();
    }
    private void FixedUpdate()
    {
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
        if (InputReader.AttackPressed)
        {
            StateMachine.ChangeState(AttackState);
            return;
        }
        if (InputReader.RollPressed)
        {
            StateMachine.ChangeState(RollState);
            return;
        }
        if (InputReader.HealPressed)
        {
            StateMachine.ChangeState(HealState);
            return;
        }
        if (InputReader.IsGuardPressed)
        {
            StateMachine.ChangeState(GuardState);
            return;
        }
        if (InputReader.MoveInput.sqrMagnitude > 0.01f)
        {
            StateMachine.ChangeState(MoveState);
            return;
        }
        StateMachine.ChangeState(IDLEState);
    }
}
