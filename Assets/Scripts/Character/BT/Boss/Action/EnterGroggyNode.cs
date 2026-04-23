using UnityEngine;

public class EnterGroggyNode : NodeBase
{
    Boss boss;

    public EnterGroggyNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        if (boss.CanTriggerFirstGroggy()) boss.SetFirstGroggyTriggered();
        else if (boss.CanTriggerSecondGroggy()) boss.SetSecondGroggyTriggered();
        else return Return(NodeState.Fail);
        boss.EnterGroggyState();
        boss.AnimController?.Trigger("Groggy");
        return Return(NodeState.Success);
    }
}
