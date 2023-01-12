using System;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public bool local = false;
    public float speed = 1f;
    private Vector3 initialPosition;
    private Vector3 upVector;

    private void Start()
    {
        initialPosition = transform.position;
        upVector = local ? transform.InverseTransformVector(Vector3.up) : Vector3.up;
    }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(speed * Time.deltaTime, upVector) * transform.rotation;
        transform.position = initialPosition + new Vector3(0, Mathf.Sin(Time.frameCount * 0.025f) * 0.15f, 0);
    }
}
