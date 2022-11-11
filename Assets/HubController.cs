using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class HubController : MonoBehaviour
{
    private List<DoorController> doors = new List<DoorController>();
    public GameObject keyCardGameObject;
    public int hubCode;

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

    [Button]
    public int LockHub()
    {
        hubCode = Random.Range(0, 9999);
        foreach (DoorController doorController in doors)
        {
            doorController.codes.Add(hubCode);
            doorController.Close();
        }

        return hubCode;
    }

    [Button]
    public void SpawnKey(int code)
    {
        GameObject keyCard = Instantiate(keyCardGameObject, transform);
        keyCard.transform.position = transform.position;
        keyCard.GetComponent<KeycardPickable>().code = code;
    }
}
