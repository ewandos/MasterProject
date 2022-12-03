using UnityEngine;

public class Goal_Movement_Teleport : Goal_Base
{
    [SerializeField] int teleportPriority = 20;
    [SerializeField] float MinAwarenessToChase = 1.5f;
    [SerializeField] float AwarenessToStopChase = 1f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float distanceBetween = 0;
    DetectableTarget CurrentTarget;
    [SerializeField] float CurrentPriority = 0;
    [SerializeField] float PriorityBuildRate = 2f;

    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;

    public override void OnTickGoal()
    {
        CurrentPriority += PriorityBuildRate * Time.deltaTime;

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

                    if (distanceBetween < stoppingDistance)
                    {
                        //CurrentPriority = candidate.Awareness < AwarenessToStopChase ? 0 : ChasePriority;
                        if (CurrentPriority > 100)
                        {
                            CurrentPriority = 100;
                        }
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
                CurrentPriority = teleportPriority;
                return;
            }
        }
    }

    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
        
        CurrentPriority = teleportPriority;
    }
    
    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        
        CurrentTarget = null;
    }

    public override int CalculatePriority()
    {
        return Mathf.FloorToInt(CurrentPriority);
    }

    public override bool CanRun()
    {
        return true;
    }
}
