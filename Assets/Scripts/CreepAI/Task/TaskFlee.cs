using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Task
{
    public class TaskFlee : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        public TaskFlee(NavMeshAgent agent, Transform transform)
        {
            _agent = agent;
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            _agent.speed = CreepBehaviorTree.fleeSpeed;
            if (_agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance)
            {
                Draw.CrossXZ(_agent.destination, 1f, Color.gray);
                State = NodeState.Running;
                return NodeState.Running;
            }
            
            
            Transform target = (Transform) GetData("player");
            float distanceToTarget = Vector3.Distance(_transform.position, target.position);
            
            if (distanceToTarget > CreepBehaviorTree.fovRange)
            {
                ClearData("player");
                State = NodeState.Success;
                return State;
            }
            
            Vector3 localFleeVector = target.position - _transform.position;
            Draw.Ray(_transform.position, -localFleeVector);
            Vector3 fleePosition = (_transform.position - localFleeVector) * CreepBehaviorTree.fleeMultiplier;

            _agent.SetDestination(fleePosition);

            Draw.Cross(_agent.destination, 2f, Color.red);
            
            State = NodeState.Running;
            return State;
        }
    }
}
