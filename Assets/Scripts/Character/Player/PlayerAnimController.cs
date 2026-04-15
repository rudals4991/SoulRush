using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator animator;
    public void Initialize(Animator animator)
    {
        Debug.Log("Anim Controller is Init");
        this.animator = animator;
    }
    public void Trigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    public void Bool(string name, bool boolean)
    {
        animator.SetBool(name, boolean);
    }
    public void Float(string name, float f)
    {
        animator.SetFloat(name, f);
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
