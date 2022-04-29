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


        if (StatTracker.Instance.getMoreMeleeAttacksPerformed())
        {
            Vector3 location = Agent.PickLocationInRange(5);
            Agent.MoveTo(location);
        }

        if (StatTracker.Instance.getMoreRangedAttacksPerformed())
        {
            //move to spot within 5 feet of player
            Agent.MoveTo(_movementGoal.MoveTarget);
        }
        
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        
        _movementGoal = null;
    }

    public override void OnTick()
    {
        if (StatTracker.Instance.getMoreMeleeAttacksPerformed())
        {
            Vector3 location = Agent.PickLocationInRange(5);
            Agent.MoveTo(location);
        }

        if (StatTracker.Instance.getMoreRangedAttacksPerformed())
        {
            //move to spot within 5 feet of player
            Agent.MoveTo(_movementGoal.MoveTarget);
        }
    }    
}
