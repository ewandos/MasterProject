using System;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up) * transform.rotation;
        transform.position = initialPosition + new Vector3(0, Mathf.Sin(Time.frameCount * 0.025f) * 0.15f, 0);
    }
}
