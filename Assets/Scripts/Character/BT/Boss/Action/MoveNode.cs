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
        Debug.Log("MoveNode Running");
        if (boss.IsTargetInStopDistance() || boss.HasExceededMaxChaseTime())
        {
            boss.Movement.Stop();
            boss.Movement.RotateToTarget();
            boss.ResetChase();
            return Return(NodeState.Success);
        }
        boss.Movement.MoveToTarget();
        boss.Movement.RotateToTarget();
        return Return(NodeState.Running);
    }
}
