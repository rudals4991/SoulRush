using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction lookAction;
    InputAction runAction;
    InputAction guardAction;
    InputAction attackAction;
    InputAction rollAction;
    InputAction healAction;
    InputAction lockOnAction;
    InputAction pauseAction;
    InputAction interactAction;

    public Vector2 MoveInput => moveAction.ReadValue<Vector2>();
    public Vector2 LookInput => lookAction.ReadValue<Vector2>();
    public bool IsRunPressed => runAction.IsPressed();
    public bool IsGuardPressed => guardAction.IsPressed();
    public bool AttackPressed => attackAction.WasPressedThisFrame();
    public bool RollPressed => rollAction.WasPressedThisFrame();
    public bool HealPressed => healAction.WasPressedThisFrame();
    public bool LockOnPressed => lockOnAction.WasPressedThisFrame();
    public bool PausePressed => pauseAction.WasPressedThisFrame();
    public bool InteractPressed => interactAction.WasPressedThisFrame();

    private void Awake()
    {
        input = new PlayerInput();
        moveAction = input.Player.Move;
        lookAction = input.Player.Look;
        runAction = input.Player.Run;
        guardAction = input.Player.Guard;
        attackAction = input.Player.Attack;
        rollAction = input.Player.Roll;
        healAction = input.Player.Heal;
        lockOnAction = input.Player.RockOn;
        pauseAction = input.Player.Pause;
        interactAction = input.Player.Interact;
    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Update()
    {
        if (AttackPressed) Debug.Log("Attack");
        if (RollPressed) Debug.Log("Roll");
        if (HealPressed) Debug.Log("Heal");
        if (IsGuardPressed) Debug.Log("Guard Holding");
    }
}
