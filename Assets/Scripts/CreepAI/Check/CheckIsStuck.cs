using CreepAI.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Check
{
    public class CheckIsStuck : Node
    {
        private NavMeshAgent _agent;
        private Transform _transform;
        private bool _value;

        public CheckIsStuck(NavMeshAgent agent, Transform transform, bool value)
        {
            _agent = agent;
            _transform = transform;
            _value = value;
        }

        public override NodeState Evaluate()
        {
            bool isStuck = Vector3.Distance(_agent.destination, _transform.position) <= 1;

            if (isStuck == _value)
            {
                State = NodeState.Success;
                return State;
            }

            State = NodeState.Failure;
            return State;
        }
    }
}
