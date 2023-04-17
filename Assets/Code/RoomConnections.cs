using System;

public class RoomConnections
{
    private bool[] connections = new bool[4];

    public RoomConnections(bool[] connections)
    {
        this.connections = connections;
    }

    public bool[] GetConnections()
    {
        return connections;
    }

    public int GetConnectionValue()
    {
        int result = 0;

        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i])
            {
                result += (int)Math.Pow(2, connections.Length - 1 - i);
            }
        }
        return result;
    }

    public void SetConnections(bool[] connections)
    {
        this.connections = connections;
    }
}