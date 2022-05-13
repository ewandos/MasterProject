using System.Collections;
using System.Collections.Generic;
using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;

namespace CreepAI.Check
{
    public class CheckLineOfSightToTarget : Node
    {
        private readonly Transform _transform;

        public CheckLineOfSightToTarget(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            Transform target = (Transform) GetData("player");
            Vector3 rayToTarget = target.position - _transform.position;
            
            Draw.Ray(_transform.position, rayToTarget, Color.gray);
            
            if (Physics.Raycast(_transform.position, rayToTarget, out var raycastHit))
            {
                // check if hit object is in the associated to the enemy layer
                if ((CreepBehaviorTree.EnemyLayer & (1 << raycastHit.transform.gameObject.layer)) > 0)
                {
                    State = NodeState.Success;
                    return State;
                }
            }

            ClearData("target");
            State = NodeState.Failure;
            return State;
        }
    }
}


