using UnityEngine;

public class BossAnimController : AnimControllerBase
{
    public override void Initialize(Animator animator)
    {
        base.Initialize(animator);
        Debug.Log("Boss Anim Controller is Init");
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
    public AnimatorStateInfo GetCurrentStateInfo(int layer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layer);
    }
    public bool IsInTransition(int layer = 0)
    {
        return animator.IsInTransition(layer);
    }
    public float GetNormalizedTime(int layer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime % 1f;
    }
    public bool IsInNormalizedTimeRange(float start, float end, int layer = 0)
    {
        if (animator.IsInTransition(layer)) return false;
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(layer).normalizedTime % 1f;
        return normalizedTime >= start && normalizedTime <= end;
    }
    public bool IsStateInNormalizedTimeRange(string stateName, float start, float end, int layer = 0)
    {
        if (animator.IsInTransition(layer)) return false;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        if (!stateInfo.IsName(stateName)) return false;
        float normalizedTime = stateInfo.normalizedTime % 1f;
        return normalizedTime >= start && normalizedTime <= end;
    }
    public void ResetTriggers(params string[] triggers)
    {
        if (triggers == null) return;
        foreach (string trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }
}
