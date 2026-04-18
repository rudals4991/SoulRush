using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rollDirection;
    float rollSpeed;
    public bool IsRolling { get; private set; }
    public bool IsInvincible { get; private set; }

    public void Initialize(Rigidbody rb, float rollSpeed)
    {
        this.rb = rb;
        this.rollSpeed = rollSpeed;
    }
    public void StartRoll(Vector3 direction)
    {
        if (direction.sqrMagnitude <= 0.001f) direction = transform.forward;
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
}
