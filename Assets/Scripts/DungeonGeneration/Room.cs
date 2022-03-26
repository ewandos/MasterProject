using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Generic,
    Kitchen,
    Quarters,
    Boss,
    Bridge
}

struct RoomLayout
{
    public Vector2 position;
    public int width;
    public int height;
    public RoomType type;
}

public class Room : MonoBehaviour
{
    // TODO Figure out, how to determine where an exit is
    public RoomType roomType = RoomType.Generic;
    public int width;
    public int height;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var test = roomType.ToString();
        
        //var roomName = Enum.GetName((RoomType)roomType);
        
        //Debug.Log($"Initialized Room: {test} with size {width.ToString()}x{height.ToString()}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRoomSize()
    {
        return (height + width) / 2f;
    }
}
