using UnityEngine;

public class IsPhaseChangeStateNode : NodeBase
{
    readonly Boss boss;

    public IsPhaseChangeStateNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        currentState = boss.IsPhaseChanging ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
