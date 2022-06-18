using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public bool startOpen;
    public bool openOnTriggerEnter;
    public bool allCodesRequired;
    public List<int> codes = new List<int>();

    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;
    private DoorStatusLightsController doorStatusLightsController;
    private DoorAudioController doorAudioController;
    private bool isClosed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
        doorStatusLightsController = GetComponentInChildren<DoorStatusLightsController>();
        doorAudioController = GetComponentInChildren<DoorAudioController>();
        isClosed = !startOpen;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        doorStatusLightsController.Initialize(codes.Count);
    }

    private void Open()
    {
        if (!isClosed) return;
        isClosed = false;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        doorAudioController.PlayOpen();
    }

    private void Close()
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
