using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public bool startOpen;
    private Animator animator;
    private BoxCollider collider;
    private NavMeshObstacle obstacle;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
        
        animator.SetBool("isOpen", startOpen);
        collider.enabled = !startOpen;
        obstacle.enabled = !startOpen;
    }

    [Button]
    public void Toggle()
    {
        animator.SetBool("isOpen", !animator.GetBool("isOpen"));
        collider.enabled = !collider.enabled;
        obstacle.enabled = !obstacle.enabled;
    }
}
