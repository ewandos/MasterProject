using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;

namespace CreepAI.Check
{
    public class CheckIsAlone : Node
    {
        private readonly Transform _transform;

        public CheckIsAlone(Transform transform)
        {
            _transform = transform;
        }
    
        public override NodeState Evaluate()
        {
            Draw.CircleXZ(_transform.position, CreepBehaviorTree.fovRange, Color.gray);
            Collider[] result = new Collider[2];
            Physics.OverlapSphereNonAlloc(_transform.position, CreepBehaviorTree.fovRange, result, CreepBehaviorTree.AllyLayer);

            if (result[1] == null)
            {
                State = NodeState.Success;
                return State;
            }
            
            State = NodeState.Failure;
            return State;
        }
    }
}
