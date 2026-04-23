using UnityEngine;

public class ChaseAttackNode : NodeBase
{
    Boss boss;

    public ChaseAttackNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null || boss.Movement == null || boss.BossAttack == null) return Return(NodeState.Fail);
        if (!boss.HasTarget()) return Return(NodeState.Fail);
        boss.StartChase();
        if (boss.IsTargetInAttackRange())
        {
            boss.Movement.Stop();
            boss.Movement.RotateToTarget();
            boss.AnimController?.Float("Speed", 0f);
            boss.ResetChase();
            boss.BossAttack.ExecuteAttack();
            return Return(NodeState.Success);
        }
        if (boss.HasExceededMaxChaseTime())
        {
            boss.Movement.Stop();
            boss.Movement.RotateToTarget();
            boss.AnimController?.Float("Speed", 0f);
            boss.ResetChase();
            boss.BossAttack.ExecuteAttack();
            return Return(NodeState.Success);
        }
        boss.Movement.MoveToTarget();
        boss.Movement.RotateToTarget();
        boss.AnimController?.Float("Speed", 1f);
        return Return(NodeState.Running);
    }
}
