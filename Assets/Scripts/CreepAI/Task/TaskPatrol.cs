using CreepAI.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Task
{
    public class TaskPatrol : Node
    {
        private NavMeshAgent _agent;
        private GameObject[] _waypoints;
        private int _currentWaypointIndex = 0;

        public TaskPatrol(NavMeshAgent agent, GameObject[] waypoints)
        {
            this._agent = agent;
            this._waypoints = waypoints;
        }
    
        public override NodeState Evaluate()
        {
            if (_agent.remainingDistance > CreepBehaviorTree.patrollingDistance) return NodeState.Running;
        
            if (_agent.remainingDistance < CreepBehaviorTree.patrollingDistance)
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        
            _agent.speed = CreepBehaviorTree.patrolSpeed;
            _agent.SetDestination(_waypoints[_currentWaypointIndex].transform.position);

            return NodeState.Running;
        }
    }
}

