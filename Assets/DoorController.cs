using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public bool startOpen;
    public bool openOnTriggerEnter;
    public int code;
    
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
        if (manager != null && manager.keychain.HasKeyFor(code))
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (openOnTriggerEnter)
        {
            Close();
        }
    }
}
