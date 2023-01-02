using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {

            Instantiate(boss,transform.position, transform.rotation);
            Destroy(this);
        }
    }
}