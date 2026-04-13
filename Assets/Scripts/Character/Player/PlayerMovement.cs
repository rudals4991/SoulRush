using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Transform cam;
    float walkSpeed;

    Vector2 moveInput;
    bool isRunning, isLockOn;
    Transform lockOnPoint;
    float rotationSpeed;
    public void Initialize(Rigidbody rb, Transform camTransform, float baseSpeed, float rotationSpeed)
    { 
        this.rb = rb;
        cam = camTransform;
        walkSpeed = baseSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    public void SetMoveInput(Vector2 input, bool running, bool lockOn, Transform targetPoint)
    { 
        moveInput = input;
        isRunning = running;
        isLockOn = lockOn;
        lockOnPoint = targetPoint;
    }
    public void StopMove()
    {
        moveInput = Vector2.zero;
        isRunning = false;
        isLockOn = false;
        lockOnPoint = null;
    }
    public void FixedTick()
    {
        if (rb == null) return;

        Vector3 moveDirection = GetCameraRelativeDirection(moveInput);
        float moveSpeed = CalculateMoveSpeed();
        Vector3 nextPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
        if (isLockOn)
        {
            RotateToLockOnPoint();
            return;
        }
        RotateToMoveDirection(moveDirection);
    }
    private float CalculateMoveSpeed()
    {
        float speed = walkSpeed;
        if (isLockOn) return speed * 0.8f;
        if (isRunning) return speed * 1.5f;
        return speed;
    }
    private Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        if (input.sqrMagnitude <= 0.01f) return Vector3.zero;
        if (cam == null) return new Vector3(input.x, 0f, input.y).normalized;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * input.y + right * input.x;
        if (moveDirection.sqrMagnitude > 1f) moveDirection.Normalize();

        return moveDirection;
    }
    private void RotateToMoveDirection(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude <= 0.001f) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        Quaternion nextRotation = Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
        rb.MoveRotation(nextRotation);
    }
    private void RotateToLockOnPoint()
    {
        if (lockOnPoint == null) return;
        Vector3 lookDirection = lockOnPoint.position - rb.position;
        lookDirection.y = 0f;
        if (lookDirection.sqrMagnitude <= 0.001f) return;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        Quaternion nextRotation = Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
        rb.MoveRotation(nextRotation);
    }
}
