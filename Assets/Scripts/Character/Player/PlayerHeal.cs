using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    public int CurrentHealCount { get; private set; } = 3;
    public float HealAmount { get; private set; } = 30f;

    public bool CanHeal()
    {
        return CurrentHealCount > 0;
    }

    public void UseHeal()
    {
        if (CurrentHealCount <= 0) return;
        CurrentHealCount--;
    }
}
