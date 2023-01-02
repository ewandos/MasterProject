using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityRenderer;
using UnityEngine;

public class DoorStatusLight : MonoBehaviour
{
    public Material valid;
    public Material invalid;

    public void SetState(bool isValid)
    {
        GetComponent<MeshRenderer>().material = isValid ? valid : invalid;
        GetComponentInChildren<Light>().enabled = true;
        GetComponentInChildren<Light>().color = isValid ? Color.green : Color.red;
    }
}
