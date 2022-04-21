using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Attack_Ranged : Goal_Base
{
    [SerializeField] int AttackPriority = 70;
    [SerializeField] float MinAwarenessToChase = 1.5f;
    [SerializeField] float AwarenessToStopChase = 1f;
    [SerializeField] public float minDistanceToAttack = 10f;
    DetectableTarget CurrentTarget;
    int CurrentPriority = 0;
    [SerializeField] public float distanceBetween = 0;
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
                    CurrentPriority = candidate.Awareness < AwarenessToStopChase ? 0 : AttackPriority;
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
                CurrentPriority = AttackPriority;
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
        return CurrentPriority + StatTracker.Instance.getMeleeAttackPerformed();
    }

    public override bool CanRun()
    {
        
        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return false;

        // check if we have anything we are aware of
        foreach(var candidate in Sensors.ActiveTargets.Values )
        {
            var agentPos = Agent.transform.position;
            distanceBetween = Vector3.Distance(candidate.RawPosition, agentPos);

            if (candidate.Awareness >= MinAwarenessToChase && distanceBetween >= minDistanceToAttack)
            {
                return true;
            }
               
        }

        return false;
    }
}
