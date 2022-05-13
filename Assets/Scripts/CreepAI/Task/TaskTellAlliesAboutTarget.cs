using CreepAI.BehaviorTree;
using UnityEngine;

namespace CreepAI.Task
{
    public class TaskTellAlliesAboutTarget : Node
    {
        public override NodeState Evaluate()
        {
            Collider[] allies = (Collider[]) GetData("neighbors");
            Transform target = (Transform) GetData("player");

            if (allies == null || target == null)
            {
                State = NodeState.Failure;
                return State;
            }
            
            foreach (Collider collider in allies)
            {
                collider.gameObject.GetComponent<CreepBehaviorTree>().Root.SetData("player", target);
                Debug.Log("Petze!!");
            }

            State = NodeState.Success;
            return State;
        }
    }
}
