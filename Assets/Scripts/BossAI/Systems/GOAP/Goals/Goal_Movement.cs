using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Movement : Goal_Base
{
    [SerializeField] int ChasePriority = 60;
    [SerializeField] float MinAwarenessToChase = 1f;
    [SerializeField] float AwarenessToStopChase = 1f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float distanceBetween = 0;
    DetectableTarget CurrentTarget;
    [SerializeField] int CurrentPriority = 0;

    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;

    public override void OnTickGoal()
    {
        CurrentPriority = 0;

        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return;

        if (CurrentTarget != null)
        {
            // check if the current is still sensed
            foreach (var candidate in Sensors.ActiveTargets.Values)
            {
                if (candidate.Detectable == CurrentTarget)
                {
                    var agentPos = Agent.transform.position;
                    distanceBetween = Vector3.Distance(candidate.RawPosition, agentPos);

                    if (distanceBetween > stoppingDistance)
                    {
                        CurrentPriority = candidate.Awareness < AwarenessToStopChase ? 0 : ChasePriority;
                    }
                    return;
                }
            }

            // clear our current target
            CurrentTarget = null;
        }

        // acquire a new target if possible
        foreach (var candidate in Sensors.ActiveTargets.Values)
        {
            // found a target to acquire
            if (candidate.Awareness >= MinAwarenessToChase)
            {
                CurrentTarget = candidate.Detectable;
                CurrentPriority = ChasePriority;
                return;
            }
        }
    }

    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        
        CurrentTarget = null;
    }

    public override int CalculatePriority()
    {
        return CurrentPriority;
    }

    public override bool CanRun()
    {
        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return false;

        // check if we have anything we are aware of
        foreach(var candidate in Sensors.ActiveTargets.Values)
        {
            var agentPos = Agent.transform.position;
            distanceBetween = Vector3.Distance(candidate.RawPosition, agentPos);

            if (candidate.Awareness >= MinAwarenessToChase 
                && distanceBetween >= stoppingDistance
                && !blocking)
            {
                return true;
            }
        }

        return false;
    }
}
