using System.Linq;
using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;

namespace CreepAI.Check
{
    public class CheckAlliesInRange : Node
    {
        private readonly Transform _transform;

        public CheckAlliesInRange(Transform transform)
        {
            _transform = transform;
        }
    
        public override NodeState Evaluate()
        {
            Draw.CircleXZ(_transform.position, CreepBehaviorTree.fovRange, Color.gray);
            Collider[] result = Physics.OverlapSphere(_transform.position, CreepBehaviorTree.fovRange, CreepBehaviorTree.AllyLayer);

            if (result.Length > 1)
            {
                Parent.Parent.SetData("neighbors", result);
                State = NodeState.Success;
                return State;
            }
            
            ClearData("neighbors");
            State = NodeState.Failure;
            return State;
        }
    }
}
