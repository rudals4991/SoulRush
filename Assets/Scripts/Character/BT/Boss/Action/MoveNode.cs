using UnityEngine;

public class MoveNode : NodeBase
{
    Boss boss;

    public MoveNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null || boss.Movement == null || !boss.HasTarget()) return Return(NodeState.Fail);
        boss.StartChase();
        if (boss.IsTargetInStopDistance() || boss.HasExceededMaxChaseTime())
        {
            boss.Movement.Stop();
            boss.Movement.RotateToTarget();
            boss.AnimController?.Float("Speed", 0f);
            boss.ResetChase();
            return Return(NodeState.Success);
        }
        boss.Movement.MoveToTarget();
        boss.Movement.RotateToTarget();
        boss.AnimController?.Float("Speed", 1f);
        return Return(NodeState.Running);
    }
}
