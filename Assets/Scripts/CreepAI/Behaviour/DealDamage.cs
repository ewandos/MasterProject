using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CreepAI.Behaviour
{
    public class DealDamage : Action
    {
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            Debug.Log("Deal Damage");
            return TaskStatus.Success;
        }
    }
}
