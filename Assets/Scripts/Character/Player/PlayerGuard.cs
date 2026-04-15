using UnityEngine;

public class PlayerGuard : MonoBehaviour
{
    public bool IsGuarding { get; private set; }

    public void StartGuard()
    {
        IsGuarding = true;
    }
    public void StopGuard()
    {
        IsGuarding = false;
    }
    public bool CanGuard()
    {
        return IsGuarding;
    }
}
