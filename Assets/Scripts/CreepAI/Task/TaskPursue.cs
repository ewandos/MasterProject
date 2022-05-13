using CreepAI.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace CreepAI.Task
{
    public class TaskPursue : Node
    {
        private Transform _transform;
        private NavMeshAgent _agent;

        public TaskPursue(Transform transform, NavMeshAgent agent)
        {
            _transform = transform;
            _agent = agent;
        }

        public override NodeState Evaluate()
        {
            // calculate relations of target's and actor's position and rotation
            Transform target = (Transform) GetData("player");
            _agent.speed = CreepBehaviorTree.pursuingSpeed;
            Vector3 targetPosition = target.transform.position;
            Vector3 targetDirection = targetPosition - _transform.position;
            float relativeHeading =
                Vector3.Angle(_transform.forward, _transform.TransformVector(target.transform.forward));
            float toTarget = Vector3.Angle(_transform.forward, _transform.TransformVector(targetDirection));
        
            // if target moves away of actor,
            // ignore calculated position and move to target position instead
            // if ((toTarget > 90 && relativeHeading < 20) || target.GetComponent<Drive>().currentSpeed < 0.01f)
            if (toTarget > 90 && relativeHeading < 20)
            {
                _agent.SetDestination(target.transform.position);
                return NodeState.Running;
            }
            
            // else calculate expected position based on lookAhead and speed
            // and move there
            // float lookAhead = targetDirection.magnitude / (_agent.speed + target.GetComponent<Drive>().currentSpeed);
            float lookAhead = 1;
            _agent.SetDestination(targetPosition + target.transform.forward * lookAhead * CreepBehaviorTree.lookAheadMultiplier);

            return NodeState.Running;
        }
    }

}

