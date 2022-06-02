using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up) * transform.rotation;
    }
}
