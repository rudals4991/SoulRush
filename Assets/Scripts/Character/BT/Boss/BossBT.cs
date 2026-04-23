using System.Collections.Generic;
using UnityEngine;

public class BossBT : MonoBehaviour
{
    Boss boss;
    NodeBase rootNode;
    public void Initialize(Boss owner)
    {
        boss = owner;
        if (boss == null) return;
        BuildTree();
    }
    private void FixedUpdate()
    {
        if (rootNode == null) return;
        rootNode.Evaluate();
    }
    private void BuildTree()
    {
        NodeBase deadSequence = new SequenceNode(new List<NodeBase>
        {
            new IsDeadNode(boss),
            new DeadNode(boss)
        });

        NodeBase groggySelector = new SelectorNode(new List<NodeBase>
        {
            new SequenceNode(new List<NodeBase>
            {
                new IsGroggyStateNode(boss),
                new GroggyNode(boss)
            }),
            new SequenceNode(new List<NodeBase>
            {
                new ShouldGroggyNode(boss),
                new EnterGroggyNode(boss)
            })
        });

        NodeBase phaseChangeSelector = new SelectorNode(new List<NodeBase>
        {
            new SequenceNode(new List<NodeBase>
            {
                new IsPhaseChangeStateNode(boss),
                new PhaseChangingNode(boss)
            }),
            new SequenceNode(new List<NodeBase>
            {
                new ShouldPhaseChangeNode(boss),
                new EnterPhaseChangeNode(boss)
            })
        });

        NodeBase attackingSelector = new SequenceNode(new List<NodeBase>
        {
            new IsAttackingStateNode(boss),
            new AttackingNode(boss)
        });

        NodeBase waitingSelector = new SequenceNode(new List<NodeBase>
        {
            new IsWaitingStateNode(boss),
            new WaitNode(boss)
        });

        NodeBase instantAttackSequence = new SequenceNode(new List<NodeBase>
        {
            new HasTargetNode(boss),
            new IsTargetInAttackRangeNode(boss),
            new AttackNode(boss)
        });

        NodeBase chaseAttackSequence = new SequenceNode(new List<NodeBase>
        {
            new HasTargetNode(boss),
            new IsTargetOutOfAttackRangeNode(boss),
            new ChaseAttackNode(boss)
        });

        NodeBase findTargetSequence = new SequenceNode(new List<NodeBase>
        {
            new FindTargetNode(boss),
            new IDLENode(boss)
        });

        NodeBase idleNode = new IDLENode(boss);

        NodeBase combatSelector = new SelectorNode(new List<NodeBase>
        {
            instantAttackSequence,
            chaseAttackSequence,
            findTargetSequence,
            idleNode
        });

        rootNode = new SelectorNode(new List<NodeBase>
        {
            deadSequence,
            groggySelector,
            phaseChangeSelector,
            attackingSelector,
            waitingSelector,
            combatSelector
        });
    }
}
