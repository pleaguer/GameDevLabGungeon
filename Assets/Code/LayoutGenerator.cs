using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    [Header("Floorproperties")]
    [SerializeField] private Vector2Int floorsize = new Vector2Int(10, 10);
    [SerializeField] private uint minRoomCount = 6;
    [SerializeField] private uint maxRoomCount = 12;
    private bool[,] layoutMap;
    private RoomConnections[,] roomConnections;

    [Header("Debug")]
    [SerializeField] private bool gizmos = true;
    [SerializeField, Range(0, 1)] float gizmoSize = 0.9f;

    private void OnValidate()
    {
        if (floorsize.x <= 0) floorsize.x = 1;
        if (floorsize.y <= 0) floorsize.y = 1;

        if (minRoomCount <= 0) minRoomCount = 1;
        if (maxRoomCount < minRoomCount) maxRoomCount = minRoomCount;

    }

    private void Awake()
    {
        layoutMap = new bool[floorsize.x, floorsize.y];
        roomConnections = new RoomConnections[floorsize.x, floorsize.y];
        Generate();
    }

    private void Generate()
    {
        // Generate Layout
        Vector2Int startPosition = new Vector2Int(Mathf.RoundToInt(floorsize.x / 2f), Mathf.RoundToInt(floorsize.y / 2f));
        Vector2Int currentPosition;
        uint roomCount = 0;

        while(roomCount < minRoomCount)
        {
            currentPosition = startPosition;
            while (roomCount < maxRoomCount)
            {
                if (currentPosition.x >= floorsize.x || currentPosition.x < 0 || currentPosition.y >= floorsize.y || currentPosition.y < 0)
                {
                    // Position is out of bounds
                    break;
                }
                else
                {
                    if (layoutMap[currentPosition.x, currentPosition.y] == false)
                    {
                        // Add room to map
                        layoutMap[currentPosition.x, currentPosition.y] = true;
                        roomCount++;
                    }
                    // Calculate new position
                    currentPosition += RandomDirection();
                }
            }
        }

        // Generate Connections
        for (int y = 0; y < floorsize.y; y++)
        {
            for (int x = 0; x < floorsize.x; x++)
            {
                if(layoutMap[x, y])
                {
                    roomConnections[x, y] = GetRoomConnections(x, y);
                }
                else
                {
                    roomConnections[x, y] = new RoomConnections(new bool[4]);
                }
            }
        }
    }

    private RoomConnections GetRoomConnections(int x, int y)
    {
        bool[] connections = new bool[4];

        // oben
        if (y + 1 < floorsize.y)
        {
            connections[3] = layoutMap[x, y + 1];
        }

        // rechts
        if (x + 1 < floorsize.x)
        {
            connections[2] = layoutMap[x + 1, y];
        }

        // unten
        if (y - 1 >= 0)
        {
            connections[1] = layoutMap[x, y - 1];
        }

        // links
        if (x - 1 >= 0)
        {
            connections[0] = layoutMap[x - 1, y];
        }
        return new RoomConnections(connections);
    }

    public RoomConnections[,] GetLayoutConnections()
    {
        return roomConnections;
    }

    public int[,] GetRoomConnectionValues()
    {
        RoomConnections[,] roomConnections = GetLayoutConnections();
        int[,] roomConnectionValues = new int[roomConnections.GetLength(0), roomConnections.GetLength(1)];
        for (int y = 0; y < roomConnections.GetLength(0); y++)
        {
            for (int x = 0; x < roomConnections.GetLength(1); x++)
            {
                roomConnectionValues[x, y] = roomConnections[x, y].GetConnectionValue();
            }
        }
        return roomConnectionValues;
    }

    private Vector2Int RandomDirection()
    {
        int multiplier;
        if(Random.value > 0.5f)
        {
            multiplier = 1;
        }
        else
        {
            multiplier = -1;
        }

        if (Random.value > 0.5f)
        {
            return Vector2Int.up * multiplier;
        }
        else
        {
            return Vector2Int.right * multiplier;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (gizmos)
        {
            // Rooms
            if (layoutMap != null)
            {
                for (int y = 0; y < floorsize.y; y++)
                {
                    for (int x = 0; x < floorsize.x; x++)
                    {
                        if (layoutMap[x, y])
                        {
                            Gizmos.DrawCube(new Vector3(x, y, 0) - Vector3.one * 0.5f, Vector3.one * gizmoSize);
                        }
                    }
                }
            }
        }
    }
}