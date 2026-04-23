using UnityEngine;

public abstract class AnimControllerBase : MonoBehaviour
{
    protected Animator animator;
    public virtual void Initialize(Animator animator)
    {
        this.animator = animator;
    }
    public virtual void Trigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    public virtual void Bool(string name, bool boolean)
    {
        animator.SetBool(name, boolean);
    }
    public virtual void Float(string name, float f)
    {
        animator.SetFloat(name, f);
    }
    public virtual void Int(string name, int value)
    { 
        animator.SetInteger(name, value);
    }
}
