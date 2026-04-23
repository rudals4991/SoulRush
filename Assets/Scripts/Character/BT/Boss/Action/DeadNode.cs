using UnityEngine;

public class DeadNode : NodeBase
{
    Boss boss;

    public DeadNode(Boss boss)
    {
        this.boss = boss;
    }
    public override NodeState Evaluate()
    {
        if (boss == null) return Return(NodeState.Fail);
        boss.Movement?.Stop();
        // 사망 애니메이션 트리거
        // boss.Animator?.PlayDead();
        return Return(NodeState.Running);
    }
}
