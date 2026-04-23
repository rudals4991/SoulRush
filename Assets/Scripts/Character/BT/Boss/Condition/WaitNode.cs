using UnityEngine;

public class WaitNode : NodeBase
{
    Boss boss;

    public WaitNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        boss.Movement?.RotateToTarget();
        boss.AnimController?.Float("Speed", 0f);
        if (boss.IsWaitFinished())
        {
            boss.ExitWaitState();
            return Return(NodeState.Success);
        }
        return Return(NodeState.Running);
    }
}
