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
        //animator.SetTrigger(trigger);
    }
    public void Bool(string name, bool boolean)
    {
        animator.SetBool(name, boolean);
    }
    public void Float(string name, float f)
    {
        animator.SetFloat(name, f);
    }
}
