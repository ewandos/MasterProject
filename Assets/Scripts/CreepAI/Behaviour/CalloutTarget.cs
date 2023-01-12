using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Drawing;
using UnityEngine;

namespace CreepAI.Behaviour
{
    public class CalloutTarget : Action
    {
        public float range = 10f;
        public SharedTransform target;
        public SharedBool foundGroup;
        public LayerMask layerMask;

        public override TaskStatus OnUpdate()
        {
            Draw.CircleXZ(transform.position, range, Color.gray);
            Collider[] result = Physics.OverlapSphere(transform.position, CreepBehaviorTree.fovRange, layerMask);

            foreach (Collider ally in result.ToList())
                ally.gameObject.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().SetVariableValue("Target", target);

            return TaskStatus.Success;
        }
    }
}
