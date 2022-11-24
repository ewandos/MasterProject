using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HubController : MonoBehaviour
{
    private List<DoorController> doors = new List<DoorController>();
    public GameObject keyCardGameObject;
    [HideInInspector] public int hubCode;
    [HideInInspector] public bool hasKey;

    private void Start()
    {
        this.doors = GetComponentsInChildren<DoorController>().ToList();
        foreach (DoorController door in doors)
        {
            List<DoorController> siblings = doors.ToList();
            siblings.Remove(door);
            door.siblingDoors = siblings;
            door.Open();
        }
    }
    
    public int LockHub()
    {
        hubCode = Random.Range(0, 9999);
        foreach (DoorController doorController in doors)
        {
            doorController.codes.Add(hubCode);
            doorController.Close(true);
        }

        return hubCode;
    }
    
    public void SpawnKey(int code)
    {
        GameObject keyCard = Instantiate(keyCardGameObject, transform);
        keyCard.transform.position = transform.position;
        keyCard.GetComponent<KeycardPickable>().code = code;
        hasKey = true;
    }
}
