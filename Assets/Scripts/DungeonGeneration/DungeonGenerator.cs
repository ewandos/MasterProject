using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 https://www.gamedeveloper.com/programming/procedural-dungeon-generation-algorithm
 
 * Ich muss Inputs angeben

Wie viele Räume soll es geben. Welche Räume sollen das sein.

Input ist also bspw. 5 Schlafräume. 2 Kantinen. 1 Brücke

Sollten insgesamt um die 50 Räume sein

Es braucht also sowohl Generic Räume, als auch spezialisierte



Räume müssen ein vorgesehenes Pattern haben

Also 2x3, oder 4x2, usw.

Beim generieren wird aus dem pool aller räume eine random größe genommen

Räume müssen entweder in jeder variante dann auch noch mehrere ausgangsvarianten haben. ODER

Räume müssen standardmäßig in alle Richtung begehbar sein, und dann nach dem generieren abgeschlossen werden
 * 
 */

public class DungeonGenerator : MonoBehaviour
{
    
    public GameObject[] rooms;
    
    public int roomCount = 10;
    public int maxRoomSize = 5;
    

    // Start is called before the first frame update
    void Start()
    {

        foreach (var room in GetMapLayout())
        {
            var position = new Vector3(room.position.x + room.width, 0, room.position.y + room.height);
            
            // TODO Get a prefab room fitting the requirements of the roomLayout
            
            var roomObj = rooms[Random.Range(0, rooms.Length)];
            
            Instantiate(roomObj, position, Quaternion.identity, transform);
        }

        /*
        var radius = CalculateRequiredGridDiameter();

        for (int i = 0; i < roomCount; i++)
        {
            Debug.Log("Spawning Room " + i);
            var room = rooms[i%rooms.Length];

            var roomGameObj = room.GetComponent<Room>();
            
            // Get random position in grid
            var point = GetRandomPointInCircle(radius);
            
            var position = new Vector3(point.x + roomGameObj.width, 0, point.y + roomGameObj.height);
            
            Instantiate(room, position, Quaternion.identity, transform);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private RoomLayout[] GetMapLayout()
    {
        // 1. Generate a list of random points in the grid
        var radius = CalculateRequiredGridDiameter();
        
        var roomList = new RoomLayout[roomCount];

        for (int i = 0; i < roomCount; i++)
        {
            roomList[i].position = GetRandomPointInCircle(radius);
            // TODO A lot of possible ways to modify room sizes
            // Have a normal distribution in order to mitigate too many small or large rooms
            // Have a maximum delta between width and height
            roomList[i].width = Random.Range(1, maxRoomSize+1);
            roomList[i].height = Random.Range(1, maxRoomSize+1);
            // TODO Set a random room type
            roomList[i].type = RoomType.Generic;
        }

        // 3. Connect the rooms with corridors
        // 4. Set room type for each room


        return roomList;
    }
    
    

    private float CalculateRequiredGridDiameter()
    {
        var gridDiameter = Mathf.Sqrt(roomCount) * CalculateAverageRoomSize();
        return Mathf.Ceil(gridDiameter);
    }

    private float CalculateAverageRoomSize()
    {
        var totalRoomSize = rooms
            .Select(room => room.GetComponent<Room>())
            .Select(room => room.GetRoomSize())
            .Aggregate(0f, (current, roomSize) => current + roomSize);

        return totalRoomSize / rooms.Length;
    }

    private Vector2 GetRandomPointInCircle(float radius)
    {
        var t = 2 * Mathf.PI * Random.Range(0f, 1f);
        var u = Random.Range(0f, 1f) + Random.Range(0f, 1f);
        var r = u > 1 ? 2 - u : u;
        var x = radius * r * Mathf.Cos(t);
        var y = radius * r * Mathf.Sin(t);
        return new Vector2(x, y);
    }
}
