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
    public UnityEvent onFirstOpen;

    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;
    private DoorStatusLightsController doorStatusLightsController;
    private bool neverOpened = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
        doorStatusLightsController = GetComponentInChildren<DoorStatusLightsController>();

        if (startOpen)
        {
            animator.SetBool("isOpen", true);
            collider.enabled = false;
            obstacle.enabled = false;
        }
        
        doorStatusLightsController.Initialize(codes.Count);
    }

    private void Open()
    {
        if (neverOpened)
            onFirstOpen.Invoke();
        
        animator.SetBool("isOpen", true);
        collider.enabled = false;
        obstacle.enabled = false;
        neverOpened = false;
    }

    private void Close()
    {
        animator.SetBool("isOpen", false);
        collider.enabled = true;
        obstacle.enabled = true;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Close();
        }
    }
}
