using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : NodeBase
{
    public SelectorNode() : base()
    {
    }
    public SelectorNode(List<NodeBase> children) : base(children)
    {
    }
    public override NodeState Evaluate()
    {
        foreach (NodeBase child in children)
        {
            NodeState result = child.Evaluate();
            switch (result)
            {
                case NodeState.Success: currentState = NodeState.Success; return currentState;
                case NodeState.Running: currentState = NodeState.Running; return currentState;
                case NodeState.Fail: continue;
            }
        }
        currentState = NodeState.Fail;
        return currentState;
    }
}
