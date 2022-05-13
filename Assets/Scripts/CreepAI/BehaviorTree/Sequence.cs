using System.Collections.Generic;

namespace CreepAI.BehaviorTree
{
    /**
     * A sequence acts like an AND-Gate.
     * It contains multiple children and expects each of them to succeed.
     * Otherwise the sequence itself fails.
     */
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        return State;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        State = NodeState.Success;
                        return State;
                }
            }

            State = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return State;
        }
    }
}