using UnityEngine;

public class IsGroggyStateNode : NodeBase
{
    readonly Boss boss;

    public IsGroggyStateNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        currentState = boss.IsGroggy ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
