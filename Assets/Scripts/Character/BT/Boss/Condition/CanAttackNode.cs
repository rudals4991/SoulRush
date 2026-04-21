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
        if (boss == null) return NodeState.Fail;
        currentState = boss.CanAttack() ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
