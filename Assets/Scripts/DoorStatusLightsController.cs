using System.Collections.Generic;
using UnityEngine;

public class DoorStatusLightsController : MonoBehaviour
{
    public List<DoorStatusLight> statusLights = new List<DoorStatusLight>();
    public float spacing = 0.1f;
    public GameObject statusLightGO;

    public void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = transform.position + new Vector3(0, -spacing * i, 0);
            GameObject obj = Instantiate(statusLightGO, transform);
            obj.transform.position = position;
            DoorStatusLight doorStatusLight = obj.GetComponent<DoorStatusLight>();
            statusLights.Add(doorStatusLight);
        }
    }

    public void DisplayDoorState(int numberOfValidKeys)
    {
        for (int i = 0; i < statusLights.Count; i++)
        {
            bool shouldSetToValid = i <= numberOfValidKeys - 1;
            statusLights[i].SetState(shouldSetToValid);
        }
    }

    public void DisplayValidState()
    {
        foreach (DoorStatusLight doorStatusLight in statusLights)
        {
            doorStatusLight.SetState(true);
        }
    }
}
