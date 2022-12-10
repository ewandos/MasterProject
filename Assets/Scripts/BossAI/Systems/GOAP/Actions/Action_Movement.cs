using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Movement : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Movement) });

    Goal_Movement _movementGoal;
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
        
        // cache the chase goal
        _movementGoal = (Goal_Movement)LinkedGoal;

        Agent.MoveTo(_movementGoal.MoveTarget);
    }

    public override void OnDeactivated()
    {
        Agent.MoveTo(Agent.transform.position);
        
        base.OnDeactivated();
        
        _movementGoal = null;
    }

    public override void OnTick()
    {
        Agent.MoveTo(_movementGoal.MoveTarget);
    }    
}
