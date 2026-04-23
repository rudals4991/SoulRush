using UnityEngine;

public class IDLENode : NodeBase
{
    Boss boss;

    public IDLENode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        return Return(NodeState.Running);
    }
}
