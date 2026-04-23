using UnityEngine;

public class GroggyNode : NodeBase
{
    Boss boss;

    public GroggyNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        if (!boss.IsGroggy) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        boss.AnimController?.Float("Speed", 0f);
        if (boss.IsGroggyFinished())
        {
            boss.ExitGroggyState();
            return Return(NodeState.Success);
        }
        return Return(NodeState.Running);
    }
}
