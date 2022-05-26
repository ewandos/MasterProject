using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
    public float destroyPossibility = 0.6f;
    void Start()
    {
        if (Random.value >= (1 - destroyPossibility))
            Destroy(gameObject);
    }
}
