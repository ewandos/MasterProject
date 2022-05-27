using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Drawing;
using UnityEngine;

namespace CreepAI.Behaviour
{
    public class ObjectsInRange : Conditional
    {
        public float fovRange;
        public LayerMask layerMask;
        public int minExpectedAmount = 0;

        public override TaskStatus OnUpdate()
        {
            Draw.CircleXZ(transform.position, fovRange, Color.gray);
            Collider[] result = Physics.OverlapSphere(transform.position, CreepBehaviorTree.fovRange, layerMask);
            return result.Length >= minExpectedAmount ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
