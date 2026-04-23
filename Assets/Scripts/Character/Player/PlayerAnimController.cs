using UnityEngine;

public class PlayerAnimController : AnimControllerBase
{
    public override void Initialize(Animator animator)
    {
        base.Initialize(animator);
        Debug.Log("Player Anim Controller is Init");
    }
    public bool IsCurrentStateFinished(int layer = 0)
    {
        if (animator.IsInTransition(layer)) return false;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        return stateInfo.normalizedTime >= 1f;
    }
    public bool IsCurrentStateName(string stateName, int layer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName);
    }
    public void ResetTrigger(string trigger)
    {
        if (animator == null) return;
        animator.ResetTrigger(trigger);
    }
    public void ResetAttackTriggers()
    {
        if (animator == null) return;

        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack4");
    }
    public AnimatorStateInfo GetCurrentStateInfo(int layer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layer);
    }
    public bool IsInTransition(int layer = 0)
    {
        return animator.IsInTransition(layer);
    }
}
