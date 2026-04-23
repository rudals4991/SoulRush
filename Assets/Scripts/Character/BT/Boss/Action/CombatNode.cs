using UnityEngine;

public class CombatNode : NodeBase
{
    Boss boss;

    public CombatNode(Boss boss)
    {
        this.boss = boss;
    }

    public override NodeState Evaluate()
    {
        if (boss == null || boss.Movement == null) return Return(NodeState.Fail);
        boss.Movement.Stop();
        boss.AnimController?.Float("Speed", 0f);
        boss.Movement.RotateToTarget();
        return Return(NodeState.Running);
    }
}
