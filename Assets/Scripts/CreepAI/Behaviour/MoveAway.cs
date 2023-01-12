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
        public SharedTransform target;
        private NavMeshAgent navMeshAgent;
        public SharedTransformList waypoints;
        private bool isRunning = false;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(target.Value.transform.position, transform.position);
            if (distanceToTarget >= stoppingDistance && isRunning)
            {
                isRunning = false;
                return TaskStatus.Success;
            }

            if (isRunning) return TaskStatus.Running;
            
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;

            NavMeshPath path = new NavMeshPath();
            Transform bestWaypoint = waypoints.Value[0];
            Vector3 bestDirection = transform.position;
            float mostDistanceToTarget = 0.0f;

            using (Draw.WithDuration(10))
            {
                Draw.Cross(transform.position, Color.gray);
            }
            
            foreach (Transform waypointTransform in waypoints.Value)
            {
                navMeshAgent.CalculatePath(waypointTransform.position, path);
                
                if (path.corners.Length > 1)
                {
                    Vector3 corner = path.corners[1];
                    Vector3 directionToCorner = Vector3.MoveTowards(transform.position, corner, 5f);

                    using (Draw.WithDuration(10))
                    {
                        Draw.Cross(directionToCorner, Color.red);
                    }
                    
                    float distanceOfFirstPathCornerToTarget = Vector3.Distance(directionToCorner, target.Value.position);
                    if (distanceOfFirstPathCornerToTarget > mostDistanceToTarget)
                    {
                        mostDistanceToTarget = distanceOfFirstPathCornerToTarget;
                        bestWaypoint = waypointTransform;
                        bestDirection = directionToCorner;
                    }
                }
            }

            Debug.Log(mostDistanceToTarget);
            navMeshAgent.SetDestination(bestWaypoint.position);
            
            using (Draw.WithDuration(10))
            {
                Draw.Cross(bestDirection, Color.green);
            }
            
            isRunning = true;
            return TaskStatus.Running;
        }
    }
}
