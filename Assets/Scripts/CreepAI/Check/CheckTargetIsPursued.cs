using CreepAI;
using CreepAI.BehaviorTree;
using Drawing;
using UnityEngine;

public class CheckTargetIsPursued : Node {
    public override NodeState Evaluate()
    {
        Transform target = (Transform) GetData("target");
        Draw.CircleXZ(target.position, CreepBehaviorTree.fovRange, Color.gray);
        Collider[] result = new Collider[2];
        Physics.OverlapSphereNonAlloc(target.position, CreepBehaviorTree.fovRange, result, CreepBehaviorTree.AllyLayer);

        if (result.Length > 1)
        {
            Debug.Log("Suceess");
            State = NodeState.Success;
            return State;
        }
            
        Debug.Log("Fail");
        State = NodeState.Failure;
        return State;
    }
}
