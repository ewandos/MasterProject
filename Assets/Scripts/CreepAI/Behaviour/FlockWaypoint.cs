using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Behaviour
{
    public class FlockWaypoint : Action
    {
        public SharedTransformList waypoints;
        public SharedInt activeWaypointIndex;
        public float strictness = 0.5f;
        public float cooldown = 10f;
        public float selectionRange = 25f;

        public float fovRange;
        public LayerMask layerMask;

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
                activeWaypointIndex.Value = SelectRandomNearWaypointIndex();

            Collider[] result = Physics.OverlapSphere(transform.position, fovRange, layerMask);
            
            int flockedWaypointIndex = 0;

            foreach (Collider ally in result)
                flockedWaypointIndex += (int) ally.gameObject.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>()
                    .GetVariable("ActiveWaypointIndex").GetValue();
            
            flockedWaypointIndex = flockedWaypointIndex %= waypoints.Value.Count;

            foreach (Collider ally in result)
                ally.gameObject.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>()
                    .SetVariableValue("ActiveWaypointIndex", flockedWaypointIndex);

            return TaskStatus.Success;
        }

        private int SelectRandomNearWaypointIndex()
        {
            List<int> validWaypointIndices = new List<int>();

            for (int i = 0; i < waypoints.Value.Count; i++)
            {
                float distance = Vector3.Distance(waypoints.Value[i].position, transform.position);
                if (distance <= selectionRange)
                    validWaypointIndices.Add(i);
            }

            int randomIndexForSelectingIndex = (int) Mathf.Floor(Random.Range(0, validWaypointIndices.Count));
            return validWaypointIndices[randomIndexForSelectingIndex];
        }
    }
}
