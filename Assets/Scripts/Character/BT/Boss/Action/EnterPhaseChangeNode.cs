using UnityEngine;

public class EnterPhaseChangeNode : NodeBase
{
    Boss boss;

    public EnterPhaseChangeNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        if (!boss.CanTriggerPhase2()) return Return(NodeState.Fail);
        boss.SetPhase2Triggered();
        boss.EnterPhaseChangeState();
        boss.AnimController?.Trigger("PhaseChange");
        return Return(NodeState.Success);
    }
}
