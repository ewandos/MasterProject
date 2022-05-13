using CreepAI.BehaviorTree;
using UnityEngine;

namespace CreepAI.Check
{
    public class CheckHasTarget : Node
    {
        private bool _value;

        public CheckHasTarget(bool value)
        {
            _value = value;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("player");
            bool hasTarget = target != null;

            if (hasTarget == _value)
            {
                State = NodeState.Success;
                return State;
            }

            State = NodeState.Failure;
            return State;
        }
    }
}
