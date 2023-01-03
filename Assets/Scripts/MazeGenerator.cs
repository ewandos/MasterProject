using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public GameSettings gameSettings;
    public HubController startHub;
    public HubController targetHub;
    public int maxLockedHubs = 5;
    public bool debug;
    
    private List<HubController> hubs = new List<HubController>();
    private HubController obstacleHub;
    private int numberOfLockedHubs = 0;
    private int iterations = 0;
    private NavMeshPath path;
    private NavMeshPath fastestPath;
    private NavMeshPath endPath;
    private int recentHubCode = 0;
    private bool needToPlaceNewObstacle = false;
    private bool finishedGeneration = false;
    void Start()
    {
        if (gameSettings.openLevel) return;
        hubs = GetComponentsInChildren<HubController>().ToList();
        hubs.Remove(startHub);
        if (debug) Debug.Log("Found " + hubs.Count + " hubs.");
        
        obstacleHub = targetHub;
        path = new NavMeshPath();
        fastestPath = new NavMeshPath();
        endPath = new NavMeshPath();
        
        NavMesh.CalculatePath(startHub.transform.position, targetHub.transform.position, NavMesh.AllAreas, fastestPath);

        NavMesh.onPreUpdate = IterateMapGeneration;
        if(debug) Debug.Log("Generation Started!");
    }

    private void FixedUpdate()
    {
        if (gameSettings.openLevel) return;
        if (fastestPath == null) return;
        
        for (int i = 0; i < fastestPath.corners.Length - 1; i++)
            if(debug) Debug.DrawLine(fastestPath.corners[i], fastestPath.corners[i + 1], Color.red);
        
        for (int i = 0; i < endPath.corners.Length - 1; i++)
            if(debug) Debug.DrawLine(endPath.corners[i], endPath.corners[i + 1], Color.green);
    }

    private void IterateMapGeneration()
    {
        if (finishedGeneration)
        {
            if (endPath.corners.Length == 0)
                NavMesh.CalculatePath(startHub.transform.position, targetHub.transform.position, NavMesh.AllAreas, endPath);
            return;
        }
            
        iterations++;
        if (iterations % 20 != 0)
            return;

        if (numberOfLockedHubs <= maxLockedHubs)
        {
            if(debug) Debug.Log("Iteration " + numberOfLockedHubs + " >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            // filter all hubs that aren't reachable
            HubController[] copiedList = new HubController[hubs.Count];
            hubs.CopyTo(copiedList);
            int countBeforeRemoval = copiedList.Length;
            foreach (HubController hubController in copiedList.ToList())
            {
                
                NavMesh.CalculatePath(startHub.transform.position, hubController.transform.position, NavMesh.AllAreas, path);
                if (path.status != NavMeshPathStatus.PathComplete)
                {
                    hubs.Remove(hubController);
                    if(debug) Debug.Log("Remove hub " + hubController + " from list.", hubController);
                }
            }

            int countOfRemovedHubs = countBeforeRemoval - hubs.Count;
            
            if(debug) Debug.Log("Removed " + countOfRemovedHubs + " hubs. " + hubs.Count  + " remaining.");

            // calculate path from start to target hub
            NavMesh.CalculatePath(startHub.transform.position, targetHub.transform.position, NavMesh.AllAreas, path);
            bool pathFound = path.status == NavMeshPathStatus.PathComplete;
            
            if (pathFound || needToPlaceNewObstacle)
            {
                if(debug) Debug.Log( "Searched Path from " + startHub + " to " + targetHub);

                // calculate path from start to obstacle hub, since this is the furthest the player is currently able to travel
                NavMesh.CalculatePath(startHub.transform.position, targetHub.transform.position, NavMesh.AllAreas, path);
                
                // determine which hubs are on the path
                List<HubController> hubsOnPath = new List<HubController>();
                foreach (HubController hubController in hubs)
                {
                    const float threshold = 9f;
                    
                    bool isOnPath = false;
                    foreach (Vector3 pathCorner in path.corners)
                    {
                        float distance = Vector3.Distance(hubController.transform.position, pathCorner);
                        if (distance <= threshold)
                        {
                            isOnPath = true;
                            break;
                        }
                    }

                    if (isOnPath)
                    {
                        hubsOnPath.Add(hubController);
                    }
                }
                
                if(debug) Debug.Log(hubsOnPath.Count + " hubs on path.");
                
                // exit generation if number of valid hubs are too few for further generation
                if (hubsOnPath.Count <= 2)
                {
                    finishedGeneration = true;
                    if(debug) Debug.Log("EXIT: No more hubs are available.");
                    return;
                }

                // take random hub on path that is in the last half of the array
                int offset = hubsOnPath.Count / 4;
                int randomIndex = Random.Range(Math.Min(0, hubsOnPath.Count - offset), hubsOnPath.Count);

                // set the random hub as new obstacle hub and lock it
                obstacleHub = hubsOnPath[randomIndex];
                hubs.Remove(obstacleHub);
                recentHubCode = obstacleHub.LockHub();
                if(debug) Debug.Log("New Obstacle Hub: " + obstacleHub + "with code " + recentHubCode, obstacleHub);
                numberOfLockedHubs++;
                needToPlaceNewObstacle = false;
            }
            else
            {
                // take random reachable hub and place keycard
                int randomIndex = Random.Range(0, hubs.Count);
                int i = 0;
                hubs[randomIndex].SpawnKey(recentHubCode);

                while (i < hubs.Count && hubs[randomIndex].hasKey)
                {
                    i++;
                    randomIndex = Random.Range(0, hubs.Count);
                }
                
                hubs[randomIndex].SpawnKey(recentHubCode);
                
                if(debug) Debug.Log("New KeyCard at: " + hubs[randomIndex] + " with code " + recentHubCode, hubs[randomIndex]);
                
                targetHub = obstacleHub;
                needToPlaceNewObstacle = true;
            }
        }
    }
}
