using UnityEngine;

public class IsTargetOutOfAttackRangeNode : NodeBase
{
    Boss boss;

    public IsTargetOutOfAttackRangeNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        return boss.IsTargetInAttackRange() ? Return(NodeState.Fail) : Return(NodeState.Success);
    }
}
