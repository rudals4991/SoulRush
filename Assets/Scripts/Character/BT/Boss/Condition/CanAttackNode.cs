using UnityEngine;

public class CanAttackNode : NodeBase
{
    Boss boss;

    public CanAttackNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        bool canAttack = boss.CanAttack();
        Debug.Log($"CanAttackNode : {canAttack}");
        return canAttack ? Return(NodeState.Success) : Return(NodeState.Fail);
    }
}
