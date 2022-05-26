using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class MoveTowards : Action
    {
        public float speed = 0;
        public float stoppingDistance = 1;
        public SharedTransform target;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(target.Value.transform.position, transform.position);
            if (navMeshAgent.stoppingDistance >= distanceToTarget)
                return TaskStatus.Success;

            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.SetDestination(target.Value.transform.position);
            return TaskStatus.Running;
        }
    }
}
