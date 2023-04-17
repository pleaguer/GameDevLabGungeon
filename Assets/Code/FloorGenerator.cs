using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    private int[,] connections;

    [Header("LayoutGenerator")]
    [SerializeField] private LayoutGenerator layoutGenerator;
    [Header("Rooms")]
    [SerializeField] private float roomoffset = 1f;

    [SerializeField] private List<GameObject> roomsT = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsR = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsD = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsL = new List<GameObject>();

    [SerializeField] private List<GameObject> roomsTD = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsRL = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsTR = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsRD = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsDL = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsTL = new List<GameObject>();

    [SerializeField] private List<GameObject> roomsTRD = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsRDL = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsTDL = new List<GameObject>();
    [SerializeField] private List<GameObject> roomsTRL = new List<GameObject>();

    [SerializeField] private List<GameObject> roomsTRDL = new List<GameObject>();


    private void Start()
    {
        connections = layoutGenerator.GetRoomConnectionValues();
        InstantiateRooms();
    }

    private void InstantiateRooms()
    {
        for (int y = 0; y < connections.GetLength(0); y++)
        {
            for (int x = 0; x < connections.GetLength(1); x++)
            {
                int roomtype = connections[x, y];
                if(roomtype != 0)
                {
                    // Instantiate Room
                    GameObject go = GetRandomRoom(connections[x, y]);
                    Instantiate(go, Vector3.right * x * roomoffset + Vector3.up * y * roomoffset, Quaternion.identity, this.transform);
                }
            }
        }
    }

    private GameObject GetRandomRoom(int type)
    {
        switch (type)
        {
            case 1:
                // Code to execute when number equals 1
                return RandomFromList(roomsT);
                break;
            case 2:
                // Code to execute when number equals 2
                return RandomFromList(roomsR);
                break;
            case 3:
                // Code to execute when number equals 3
                return RandomFromList(roomsTR);
                break;
            case 4:
                // Code to execute when number equals 4
                return RandomFromList(roomsD);
                break;
            case 5:
                // Code to execute when number equals 5
                return RandomFromList(roomsTD);
                break;
            case 6:
                // Code to execute when number equals 6
                return RandomFromList(roomsRD);
                break;
            case 7:
                // Code to execute when number equals 7
                return RandomFromList(roomsTRD);
                break;
            case 8:
                // Code to execute when number equals 8
                return RandomFromList(roomsL);
                break;
            case 9:
                // Code to execute when number equals 9
                return RandomFromList(roomsTL);
                break;
            case 10:
                // Code to execute when number equals 10
                return RandomFromList(roomsRL);
                break;
            case 11:
                // Code to execute when number equals 11
                return RandomFromList(roomsTRL);
                break;
            case 12:
                // Code to execute when number equals 12
                return RandomFromList(roomsDL);
                break;
            case 13:
                // Code to execute when number equals 13
                return RandomFromList(roomsTDL);
                break;
            case 14:
                // Code to execute when number equals 14
                return RandomFromList(roomsRDL);
                break;
            case 15:
                // Code to execute when number equals 15
                return RandomFromList(roomsTRDL);
                break;
            default:
                // Code to execute when number doesn't match any case
                throw new System.IndexOutOfRangeException($"Invalid roomtype with value {type}.");
                break;
        }
    }

    private T RandomFromList<T>(List<T> list)
    {
        int index = Random.RandomRange(0, list.Count);
        return list[index];
    }
}
