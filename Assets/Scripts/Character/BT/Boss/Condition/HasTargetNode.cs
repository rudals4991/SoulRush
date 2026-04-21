using UnityEngine;

public class HasTargetNode : NodeBase
{
    Boss boss;

    public HasTargetNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return NodeState.Fail;
        currentState = boss.HasTarget() ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
