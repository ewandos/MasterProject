using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DoorSpawner : MonoBehaviour
{
    private List<Transform> doorSpawns = new List<Transform>();
    public GameObject doorGO;
    private NavMeshAgent navMeshAgent;
    public Transform targetTransform;

    private void Start()
    {
        this.doorSpawns = GetComponentsInChildren<Transform>().ToList();
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private bool IsLevelCompletable()
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(targetTransform.position, path);
        return navMeshAgent.hasPath;
    }
}
