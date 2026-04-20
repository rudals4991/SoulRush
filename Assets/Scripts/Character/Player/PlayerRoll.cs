using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rollDirection;
    float rollSpeed;
    PlayerStamina stamina;
    const float rollStaminaCost = 20f;
    public bool IsRolling { get; private set; }
    public bool IsInvincible { get; private set; }

    public void Initialize(Rigidbody rb, float rollSpeed, PlayerStamina stamina)
    {
        this.rb = rb;
        this.rollSpeed = rollSpeed;
        this.stamina = stamina;
    }
    public void StartRoll(Vector3 direction)
    {
        if (direction.sqrMagnitude <= 0.001f) direction = transform.forward;
        UseRollStamina();
        rollDirection = direction.normalized;
        IsRolling = true;
        IsInvincible = false;
    }
    public void Tick(float deltaTime)
    {
        if (!IsRolling) return;
        if (rb == null) return;
        Vector3 nextPosition = rb.position + rollDirection * rollSpeed * deltaTime;
        rb.MovePosition(nextPosition);
        Quaternion targetRotation = Quaternion.LookRotation(rollDirection);
        rb.MoveRotation(targetRotation);
    }
    public void EndRoll()
    {
        IsRolling = false;
        IsInvincible = false;
        rollDirection = Vector3.zero;
    }
    public void SetInvincible(bool value)
    {
        IsInvincible = value;
    }
    void UseRollStamina()
    {
        if (stamina == null) return;
        if (stamina.CanUse(rollStaminaCost)) stamina.TryUse(rollStaminaCost, true);
        else stamina.ForceUse(rollStaminaCost, true);
    }
}
