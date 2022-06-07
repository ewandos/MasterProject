using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class MoveToWaypoint : Action
    {
        public SharedInt activeWaypointIndex;
        public SharedTransformList waypoints;
        public float speed = 1f;
        public float stoppingDistance = 1f;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 activeWaypointPosition = waypoints.Value[activeWaypointIndex.Value].position;
            float distanceToTarget = Vector3.Distance(activeWaypointPosition, transform.position);
            if (navMeshAgent.stoppingDistance >= distanceToTarget)
                return TaskStatus.Running;

            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.SetDestination(activeWaypointPosition);
            return TaskStatus.Success;
        }
    }
}
