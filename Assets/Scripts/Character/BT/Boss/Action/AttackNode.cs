using UnityEngine;

public class AttackNode : NodeBase
{
    Boss boss;

    public AttackNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null || boss.BossAttack == null) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        boss.Movement?.RotateToTarget();
        boss.BossAttack.ExecuteAttack();
        return Return(NodeState.Success);
    }
}
