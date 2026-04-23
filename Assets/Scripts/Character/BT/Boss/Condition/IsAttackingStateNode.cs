using UnityEngine;

public class IsAttackingStateNode : NodeBase
{
    Boss boss;

    public IsAttackingStateNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        return boss.IsAttacking ? Return(NodeState.Success) : Return(NodeState.Fail);
    }
}
