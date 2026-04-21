using UnityEngine;

public class IsTargetInAttackRangeNode : NodeBase
{
    Boss boss;

    public IsTargetInAttackRangeNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return NodeState.Fail;
        currentState = boss.IsTargetInAttackRange() ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
