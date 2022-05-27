using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class Bait : Action
    {
        public SharedTransform target;
        public float range = 15;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.Value.position);
            Draw.CircleXZ(transform.position, range);
            if (distanceToTarget >= range)
            {
                navMeshAgent.isStopped = true;
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }
    }
}
