using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private HealthSystem healthSystem;

    private void Awake()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        List<Transform> w = new List<Transform>();
        foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            w.Add(waypoint.transform);
        }
        behaviorTree.SetVariableValue("Waypoints", w);
    }
}
