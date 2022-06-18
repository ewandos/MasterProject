using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class MoveAway : Action
    {
        public float speed = 1;
        public float stoppingDistance = 5;
        public float multiplier = 1;
        public SharedTransform target;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(target.Value.transform.position, transform.position);
            if (distanceToTarget >= stoppingDistance)
                return TaskStatus.Success;

            Vector3 localAwayVector = target.Value.position - transform.position;
            Vector3 moveDestination = transform.position - localAwayVector * multiplier ;

            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;

            NavMeshHit hit;
            NavMesh.SamplePosition(moveDestination, out hit, 50f, NavMesh.AllAreas);
            
            bool successful = navMeshAgent.SetDestination(hit.position);
            return TaskStatus.Running;
        }
    }
}
