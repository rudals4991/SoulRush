using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Boss boss;
    public void Initialize(Boss owner)
    { 
        boss = owner;
    }
    public void TryAttack()
    {
        if (!boss.CanAttack()) return;
        boss.StartAttack();
    }
}
