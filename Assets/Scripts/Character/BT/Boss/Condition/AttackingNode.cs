using UnityEngine;

public class AttackingNode : NodeBase
{
    Boss boss;

    public AttackingNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null || boss.AnimController == null) return Return(NodeState.Fail);
        if (!boss.IsAttacking) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        boss.AnimController?.Float("Speed", 0f);
        if (boss.AnimController.IsCurrentStateFinished())
        {
            boss.BossAttack?.FinishAttack();
            return Return(NodeState.Success);
        }
        return Return(NodeState.Running);
    }
}
