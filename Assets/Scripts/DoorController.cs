using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public UnityEvent onOpen;
    public bool startClosed;
    public bool openOnTriggerEnter;
    public bool allCodesRequired;
    public bool consumesKeycard;
    public bool acceptEveryKeycard;
    public List<int> codes = new List<int>();
    public List<DoorController> siblingDoors = new List<DoorController>();
    
    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;
    private DoorStatusLightsController doorStatusLightsController;
    private DoorAudioController doorAudioController;
    private bool isClosed;
    private bool wasUnlocked;

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

    public void Initialize()
    {
        doorStatusLightsController.Initialize(codes.Count);
    }

    public void Open(bool silent = false)
    {
        if (!isClosed) return;
        wasUnlocked = true;
        isClosed = false;
        onOpen.Invoke();
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        if (!silent) doorAudioController.PlayOpen();

        foreach (DoorController doorController in siblingDoors)
        {
            doorController.Open(true);
        }
    }

    public void Close(bool silent = false)
    {
        isClosed = true;
        animator.SetBool("isClosed", isClosed);
        collider.enabled = isClosed;
        obstacle.enabled = isClosed;
        if(!silent) doorAudioController.PlayClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (openOnTriggerEnter)
        {
            Open();
            doorStatusLightsController.DisplayValidState();
            return;
        }
        
        if (wasUnlocked) return;
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager != null)
            EvaluateKeychain(manager);
    }

    private void EvaluateKeychain(PlayerManager manager)
    {
        List<bool> foundCodeResults = new List<bool>();
        foreach (int code in codes)
            foundCodeResults.Add( manager.keychain.HasKeyFor(code));

        bool oneKeycardMatches = foundCodeResults.Contains(true);
        bool everyKeycardMatches = !foundCodeResults.Contains(false);
        bool numberOfKeycardMatches = manager.keychain.GetKeycardCount() >= codes.Count;

        if (manager.keychain.hasMasterKey 
            || allCodesRequired && everyKeycardMatches 
            || !allCodesRequired && oneKeycardMatches
            || acceptEveryKeycard && numberOfKeycardMatches)
        {
            wasUnlocked = true;
            Open();
            doorStatusLightsController.DisplayValidState();
            if (consumesKeycard) ConsumeKeycards(manager.keychain);
            return;
        }

        int numberOfValidKeys = acceptEveryKeycard ? 
            manager.keychain.GetKeycardCount() 
            : foundCodeResults.Count(foundCodeResult => foundCodeResult);
        
        doorStatusLightsController.DisplayDoorState(numberOfValidKeys);
        
        if (numberOfValidKeys < codes.Count)
            doorAudioController.PlayLocked();
    }

    private void ConsumeKeycards(Keychain keychain)
    {
        if (acceptEveryKeycard)
            keychain.RemoveNumberOfCodes(codes.Count);
        else
            foreach (int code in codes)
                keychain.RemoveCode(code);
    }

    private void OnTriggerExit(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Close();
        }
    }
}
