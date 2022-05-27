using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace CreepAI.Behaviour
{
    public class HasTarget : Conditional
    {
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            return target.Value == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
