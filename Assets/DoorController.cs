using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public bool startOpen;
    public bool openOnTriggerEnter;
    public List<int> codes = new List<int>();
    public bool allCodesRequired;
    
    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();

        if (startOpen)
        {
            animator.SetBool("isOpen", true);
            collider.enabled = false;
            obstacle.enabled = false;
        }
    }

    public void Open()
    {
        animator.SetBool("isOpen", true);
        collider.enabled = false;
        obstacle.enabled = false;
    }

    public void Close()
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
            Open();
        
        if (!allCodesRequired && foundCodeResults.Contains(true))
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Close();
        }
    }
}
