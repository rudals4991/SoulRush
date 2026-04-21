using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : NodeBase
{
    public SequenceNode() : base()
    {
    }
    public SequenceNode(List<NodeBase> children) : base(children)
    {
    }
    public override NodeState Evaluate()
    {
        bool hasRunningNode = false;
        foreach (NodeBase child in children)
        {
            NodeState result = child.Evaluate();
            switch (result)
            {
                case NodeState.Fail: currentState = NodeState.Fail; return currentState;
                case NodeState.Running: hasRunningNode = true; break;
                case NodeState.Success: continue;
            }
        }
        currentState = hasRunningNode ? NodeState.Running : NodeState.Success;
        return currentState;
    }
}
