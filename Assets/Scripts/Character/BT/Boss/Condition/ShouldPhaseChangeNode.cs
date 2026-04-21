using UnityEngine;

public class ShouldPhaseChangeNode : NodeBase
{
    Boss boss;

    public ShouldPhaseChangeNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return NodeState.Fail;
        currentState = boss.CanTriggerPhase2() ? NodeState.Success : NodeState.Fail;
        return currentState;
    }
}
