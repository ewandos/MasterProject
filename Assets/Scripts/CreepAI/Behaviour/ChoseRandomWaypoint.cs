using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
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

        public override void OnAwake()
        {
            List<Transform> w = new List<Transform>();
            foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
            {
                w.Add(waypoint.transform);
            }

            Debug.Log("DDDDD");
            waypoints.Value = w;
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
