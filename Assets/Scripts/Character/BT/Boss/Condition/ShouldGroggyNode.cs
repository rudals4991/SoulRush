using UnityEngine;

public class ShouldGroggyNode : NodeBase
{
    Boss boss;

    public ShouldGroggyNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null)return NodeState.Fail;
        bool canFirstGroggy = boss.CanTriggerFirstGroggy();
        bool canSecondGroggy = boss.CanTriggerSecondGroggy();
        currentState = (canFirstGroggy || canSecondGroggy) ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
