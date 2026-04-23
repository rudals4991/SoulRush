using UnityEngine;

public class PhaseChangingNode : NodeBase
{
    Boss boss;

    public PhaseChangingNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        if (!boss.IsPhaseChanging) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        boss.AnimController?.Float("Speed", 0f);
        if (boss.IsPhaseChangeFinished())
        {
            boss.ExitPhaseChangeState();
            return Return(NodeState.Success);
        }
        return Return(NodeState.Running);
    }
}
