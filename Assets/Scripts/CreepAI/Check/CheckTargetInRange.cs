using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;

namespace CreepAI.Check
{
    public class CheckTargetInRange : Node
    {
        private readonly Transform _transform;

        public CheckTargetInRange(Transform transform)
        {
            _transform = transform;
        }
    
        public override NodeState Evaluate()
        {
            object t = GetData("player");
            if (t == null)
            {
                Collider[] results = new Collider[1];
                Physics.OverlapSphereNonAlloc(_transform.position, CreepBehaviorTree.fovRange, results, CreepBehaviorTree.EnemyLayer);
                Draw.CircleXZ(_transform.position, CreepBehaviorTree.fovRange, Color.gray);
                if (results[0] != null)
                {
                    Parent.Parent.SetData("player", results[0].transform);
                    State = NodeState.Success;
                    return State;
                }
            
                State = NodeState.Failure;
                return State;
            }
            
            Transform target = (Transform) t;
            float distanceToTarget = Vector3.Distance(target.position, _transform.position);
            if (distanceToTarget > CreepBehaviorTree.fovRange)
            {
                ClearData("player");
                State = NodeState.Failure;
                return State;
            }

            Draw.CircleXZ(_transform.position, CreepBehaviorTree.fovRange, Color.red);
            State = NodeState.Success;
            return State;
        }
    }

}

