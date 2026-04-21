using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase
{
    protected NodeState currentState;
    protected NodeBase parent;
    protected List<NodeBase> children = new();
    public NodeState CurrentState => currentState;
    public NodeBase Parent => parent;
    public IReadOnlyList<NodeBase> Children => children;

    protected NodeBase() { }
    protected NodeBase(List<NodeBase> children)
    {
        foreach (NodeBase child in children)
        {
            AddChild(child);
        }
    }
    public void AddChild(NodeBase child)
    {
        if (child == null) return;
        child.parent = this;
        children.Add(child);
    }
    public abstract NodeState Evaluate();
}
