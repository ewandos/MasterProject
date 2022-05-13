using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Task
{
    public class TaskWander : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        public TaskWander(NavMeshAgent agent, Transform transform)
        {
            _agent = agent;
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            if (_agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance)
            {
                Draw.CrossXZ(_agent.destination, 1f, Color.gray);
                State = NodeState.Running;
                return NodeState.Running;
            }

            _agent.speed = CreepBehaviorTree.wanderSpeed;
            Vector3 randomPosition = Random.insideUnitSphere * CreepBehaviorTree.wanderRadius;
            randomPosition += _transform.position;

            Vector3 finalPosition;
            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, CreepBehaviorTree.wanderRadius, 1))
            {
                finalPosition = hit.position;
            }
            else
            {
                State = NodeState.Failure;
                return NodeState.Failure;
            }

            _agent.SetDestination(finalPosition);
            
            State = NodeState.Success;
            return NodeState.Success;
        }
    }
}
