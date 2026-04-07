using UnityEngine;

public class Player : CharacterBase
{
    public Animator Animator { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerGuard Guard { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerRoll Roll { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIDLEState IDLEState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerHealState HealState { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        Animator = GetComponent<Animator>();
        Movement = GetComponent<PlayerMovement>();
        Guard = GetComponent<PlayerGuard>();
        PlayerAttack = GetComponent<PlayerAttack>();
        Roll = GetComponent<PlayerRoll>();

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
    }

    private void Update()
    {
        StateMachine.Update();
    }
}
