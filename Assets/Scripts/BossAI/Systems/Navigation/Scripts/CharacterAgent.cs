using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EOffmeshLinkStatus
{
    NotStarted,
    InProgress
}

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAgent : CharacterBase
{
    [SerializeField] float NearestPointSearchRange = 5f;
    [SerializeField] GameObject player;

    NavMeshAgent Agent;
    private Animator anim;
    bool DestinationSet = false;
    bool ReachedDestination = false;
    EOffmeshLinkStatus OffMeshLinkStatus = EOffmeshLinkStatus.NotStarted;
    private float standardSpeed = 3.5f;
    private float standardAcceleration = 8f;
    int wait = 3;
    int waitTix = 3;
    
    public bool IsMoving => Agent.velocity.magnitude > float.Epsilon;

    public bool AtDestination => ReachedDestination;

    // Start is called before the first frame update
    protected void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (--wait > 0) return;
        if (--wait < 0) wait = 0;
        
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = transform.position.y;
        
        anim.SetBool("isRunning", IsMoving);


        // have a path and near the end point?
        if (!Agent.pathPending && !Agent.isOnOffMeshLink && DestinationSet)
        {
            if ((Agent.remainingDistance <= Agent.stoppingDistance))
            {
                DestinationSet = false;
                ReachedDestination = true;
                changeSpeed(standardSpeed, standardAcceleration);
                wait = waitTix;
            }
        }

        // are we on an offmesh link?
        if (Agent.isOnOffMeshLink)
        {
            // have we started moving along the link
            if (OffMeshLinkStatus == EOffmeshLinkStatus.NotStarted)
                StartCoroutine(FollowOffmeshLink());
        }
    }

    IEnumerator FollowOffmeshLink()
    {
        // start the offmesh link - disable NavMesh agent control
        OffMeshLinkStatus = EOffmeshLinkStatus.InProgress;
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        // move along the path
        Vector3 newPosition = transform.position;
        while (!Mathf.Approximately(Vector3.Distance(newPosition, Agent.currentOffMeshLinkData.endPos), 0f))
        {
            newPosition = Vector3.MoveTowards(transform.position, Agent.currentOffMeshLinkData.endPos, Agent.speed * Time.deltaTime);
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }

        // flag the link as completed
        OffMeshLinkStatus = EOffmeshLinkStatus.NotStarted;
        Agent.CompleteOffMeshLink();

        // return control the agent
        Agent.updatePosition = true;
        Agent.updateRotation = true;
        Agent.updateUpAxis = true;    
    }

    public Vector3 PickLocationInRange(float range)
    {
        Vector3 searchLocation = transform.position;
        searchLocation += Random.Range(-range, range) * Vector3.forward;
        searchLocation += Random.Range(-range, range) * Vector3.right;

        NavMeshHit hitResult;
        if (NavMesh.SamplePosition(searchLocation, out hitResult, NearestPointSearchRange, NavMesh.AllAreas))
            return hitResult.position;

        return transform.position;
    }

    protected virtual void CancelCurrentCommand()
    {
        // clear the current path
        Agent.ResetPath();

        DestinationSet = false;
        ReachedDestination = false;
        OffMeshLinkStatus = EOffmeshLinkStatus.NotStarted;
    }

    public virtual void MoveTo(Vector3 destination)
    {
        CancelCurrentCommand();
        SetDestination(destination);
    }
    
    public virtual void MoveTo(Vector3 destination, float speed, float acceleration)
    {
        CancelCurrentCommand();
        changeSpeed(speed, acceleration);
        SetDestination(destination);
    }

    public virtual void SetDestination(Vector3 destination)
    {
        // find nearest spot on navmesh and move there
        NavMeshHit hitResult;
        if (NavMesh.SamplePosition(destination, out hitResult, NearestPointSearchRange, NavMesh.AllAreas))
        {
            Agent.SetDestination(hitResult.position);
            DestinationSet = true;
            ReachedDestination = false;
        }
    }
    
    public virtual void changeSpeed(float newSpeed, float acceleration)
    {
        Agent.speed = newSpeed;
        Agent.acceleration = acceleration;
    }
    
    public virtual float getSpeed()
    {
        return Agent.speed;
    }
}
