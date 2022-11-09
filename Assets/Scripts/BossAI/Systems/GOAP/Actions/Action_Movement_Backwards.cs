using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Movement_Backwards : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Movement_Backwards) });

    Goal_Movement _movementGoal;
    [SerializeField] float SearchRange = 10f;
    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);
        Animator anim = GetComponent<Animator>();
        
        anim.Play("BossArmature_jump_back");
        
        Vector3 location = Agent.PickLocationBehind(SearchRange);

        Agent.transform.position = location;
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        
        _movementGoal = null;
    }

    public override void OnTick()
    {
        if (Agent.AtDestination)
            OnActivated(LinkedGoal);
        
    }    
}
