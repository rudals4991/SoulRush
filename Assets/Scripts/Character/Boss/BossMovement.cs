using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Boss boss;
    Rigidbody rb;
    public void Initialize(Boss owner, Rigidbody rb)
    { 
        boss = owner;
        this.rb = rb;
    }
    public void MoveToTarget()
    {
        if (boss.Target == null) return;
        if (boss.IsTargetInStopDistance()) return;
        Vector3 direction = boss.Target.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.001f) return;
        direction.Normalize();
        Vector3 nextPosition = rb.position + direction * boss.BossStat.baseMoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }
    public void Stop()
    {
        rb.linearVelocity = Vector3.zero;
    }
    public void RotateToTarget()
    {
        if (boss.Target == null) return;
        Vector3 direction = boss.Target.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.001f) return;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }
}
