using UnityEngine;

public class FindTargetNode : NodeBase
{
    Boss boss;

    public FindTargetNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        if (boss.HasTarget()) return Return(NodeState.Success);
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null) return Return(NodeState.Fail);
        boss.TryFindTarget(playerObject.transform);
        return boss.HasTarget() ? Return(NodeState.Success) : Return(NodeState.Fail);
    }
}
