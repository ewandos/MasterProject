using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class ChoseRandomWaypoint : Action
    {
        public SharedTransformList waypoints;
        public SharedInt activeWaypointIndex;
        public float strictness = 0.5f;
        public float cooldown = 10f;

        private float timer;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                return TaskStatus.Success;
            }

            timer = cooldown;

            if (Random.Range(0f, 1f) > strictness || activeWaypointIndex.Value == -1)
                activeWaypointIndex.Value = (int)Mathf.Floor(Random.Range(0, waypoints.Value.Count));
            
            return TaskStatus.Success;
        }
    }
}
