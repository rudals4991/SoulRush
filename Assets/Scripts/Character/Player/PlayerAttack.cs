using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking { get; private set; }
    public int CurrentCombo { get; private set; }
    public bool IsNextCombo { get; private set; }
    public int maxCombo = 4;
    public void StartFirstAttack()
    {
        IsAttacking = true;
        CurrentCombo = 1;
        IsNextCombo = false;
    }
    public void StartNextCombo()
    {
        if (CurrentCombo >= maxCombo) return;
        CurrentCombo++;
        IsNextCombo = false;
    }
    public void QueueNextCombo()
    {
        if (!IsAttacking) return;
        if (CurrentCombo >= maxCombo) return;

        IsNextCombo = true;
    }
    public bool CanNextCombo()
    {
        return IsAttacking && CurrentCombo < maxCombo;
    }
    public void EndAttack()
    {
        IsAttacking = false;
        CurrentCombo = 0;
        IsNextCombo = false;
    }
}
