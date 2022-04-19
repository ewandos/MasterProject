using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Retreat : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Retreat) });

    Goal_Retreat RetreatGoal;
    [SerializeField] float RetreatRange = 10f;
    

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

        Vector3 location = Agent.PickLocationInRange(RetreatRange);

        Agent.MoveTo(location);
    }

    public override void OnTick()
    {
        // arrived at destination?
        if (Agent.AtDestination)
            OnActivated(LinkedGoal);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        
        RetreatGoal = null;
    }
    
}