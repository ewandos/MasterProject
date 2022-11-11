using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public bool startClosed;
    public bool openOnTriggerEnter;
    public bool allCodesRequired;
    public List<int> codes = new List<int>();
    public List<DoorController> siblingDoors = new List<DoorController>();
    
    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;
    private DoorStatusLightsController doorStatusLightsController;
    private DoorAudioController doorAudioController;
    private bool isClosed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
        doorStatusLightsController = GetComponentInChildren<DoorStatusLightsController>();
        doorAudioController = GetComponentInChildren<DoorAudioController>();
        isClosed = startClosed;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        doorStatusLightsController.Initialize(codes.Count);
    }

    public void Open()
    {
        if (!isClosed) return;
        isClosed = false;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        doorAudioController.PlayOpen();

        foreach (DoorController doorController in siblingDoors)
        {
            doorController.Open();
        }
    }

    public void Close()
    {
        isClosed = true;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        doorAudioController.PlayClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Open();
            doorStatusLightsController.DisplayValidState();
            return;
        }

        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager != null)
            EvaluateKeychain(manager);
    }

    private void EvaluateKeychain(PlayerManager manager)
    {
        List<bool> foundCodeResults = new List<bool>();
        foreach (int code in codes)
            foundCodeResults.Add( manager.keychain.HasKeyFor(code));

        if (allCodesRequired && !foundCodeResults.Contains(false))
        {
            doorStatusLightsController.DisplayValidState();
            Open();
        }
        
        if (!allCodesRequired && foundCodeResults.Contains(true))
        {
            Open();
            doorStatusLightsController.DisplayValidState();
        }

        int numberOfValidKeys = foundCodeResults.Count(foundCodeResult => foundCodeResult);
        doorStatusLightsController.DisplayDoorState(numberOfValidKeys);
        
        if (numberOfValidKeys < codes.Count)
            doorAudioController.PlayLocked();
    }

    private void OnTriggerExit(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Close();
        }
    }
}
