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

        NodeBase attackSequence = new SequenceNode(new List<NodeBase>
        {
            new HasTargetNode(boss),
            new CanAttackNode(boss),
            new IsTargetInAttackRangeNode(boss),
            new AttackNode(boss)
        });

        NodeBase chaseAndAttackSequence = new SequenceNode(new List<NodeBase>
        {
            new HasTargetNode(boss),
            new IsTargetOutOfAttackRangeNode(boss),
            new MoveNode(boss),
            new CanAttackNode(boss),
            new AttackNode(boss)
        });

        NodeBase combatIdleSequence = new SequenceNode(new List<NodeBase>
        {
            new HasTargetNode(boss),
            new CombatNode(boss)
        });

        NodeBase findTargetSequence = new SequenceNode(new List<NodeBase>
        {
            new FindTargetNode(boss),
            new IDLENode(boss)
        });

        NodeBase idleNode = new IDLENode(boss);

        NodeBase combatSelector = new SelectorNode(new List<NodeBase>
        {
            attackSequence,
            chaseAndAttackSequence,
            combatIdleSequence,
            findTargetSequence,
            idleNode
        });

        rootNode = new SelectorNode(new List<NodeBase>
        {
            deadSequence,
            groggySelector,
            phaseChangeSelector,
            combatSelector
        });
    }
}
