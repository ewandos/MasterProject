using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Movement_Backwards : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Movement) });

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
        
        // cache the chase goal
        _movementGoal = (Goal_Movement)LinkedGoal;

        //TODO: anim here
        
        
        if (StatTracker.Instance.getMoreRangedAttacksPerformed())
        {
            Agent.MoveTo(_movementGoal.MoveTarget);
        }
        else
        {
            //move away from player
            Vector3 location = Agent.PickLocationInRange(SearchRange);

            Agent.MoveTo(location);
        }

    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        
        _movementGoal = null;
    }

    public override void OnTick()
    {
        //move away from player
        //Vector3 location = Agent.PickLocationInRange(SearchRange);

        //Agent.MoveTo(location);
        
    }    
}
