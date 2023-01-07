using UnityEngine;

public class CreepCorpse : MonoBehaviour
{
    private Transform playerTransform;
    public float destroyRange = 20;
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerManager>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) >= destroyRange)
            Destroy(gameObject);
    }
}
