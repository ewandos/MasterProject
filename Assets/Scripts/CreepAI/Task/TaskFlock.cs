using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Task
{
    public class TaskFlock : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        public TaskFlock(NavMeshAgent agent, Transform transform)
        {
            _agent = agent;
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            Collider[] neighbors = (Collider[]) GetData("neighbors");
            
            if (neighbors == null || neighbors.Length == 0)
            {
                State = NodeState.Failure;
                return State;
            }

            Vector3 destinationSum = Vector3.zero;

            for (int i = 0; i < neighbors.Length; i++)
            {
                destinationSum += neighbors[i].GetComponent<NavMeshAgent>().destination;
            }

            Vector3 averageDestination = destinationSum / neighbors.Length;

            // Move
            
            _agent.SetDestination(averageDestination);
            Draw.CrossXZ(_agent.destination, 3f, Color.red);
            State = NodeState.Running;
            return State;
        }
    }
}
