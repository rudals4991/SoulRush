using UnityEngine;

public class IsDeadNode : NodeBase
{
    Boss boss;

    public IsDeadNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        currentState = boss.IsDead ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
