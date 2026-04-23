using UnityEngine;

public class IsWaitingStateNode : NodeBase
{
    Boss boss;

    public IsWaitingStateNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        return boss.IsWaiting ? Return(NodeState.Success) : Return(NodeState.Fail);
    }
}
