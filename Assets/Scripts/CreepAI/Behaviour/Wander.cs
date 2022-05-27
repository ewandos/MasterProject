using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class Wander : Action
    {
        public float speed = 1f;
        public float radius = 4f;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (navMeshAgent.hasPath && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                return TaskStatus.Running;

            Vector3 randomPosition = Random.insideUnitSphere * radius;
            randomPosition += transform.position;

            Vector3 finalPosition;
            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, radius, 1))
                finalPosition = hit.position;
            else
                return TaskStatus.Failure;
            
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(finalPosition);
            return TaskStatus.Success;
        }
    }
}
