using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private int buildIndex;
    //index 0 is north, 1 is east, 2 is south, 3 is west
    private bool[] doors;
    private Room[] neighbors;

    public Room(int index, string doorCode)
    {
        buildIndex = index;
        neighbors = new Room[4];
        doors = new bool[4];

        //initialize each to null
        for(int i = 0;i < doors.Length;i++)
        {
            doors[i] = doorCode[i] == '1';
            neighbors[i] = null;
        }
    }

    public int getBuildIndex()
    {
        return buildIndex;
    }

    public void setBuildIndex(int newIndex)
    {
        buildIndex = newIndex;
    }

    public bool[] getDoors()
    {
        return doors;
    }

    public bool hasDoor(int direction)
    {
        return doors[direction];
    }

    public Room[] getNeighbors()
    {
        return neighbors;
    }

    public Room getNeighbor(int direction)
    {
        return neighbors[direction];
    }

    public void setNeighbor(int direction, Room neighbor)
    {
        neighbors[direction] = neighbor;
    }

    public bool hasNeighbor(int direction)
    {
        return neighbors[direction] != null;
    }
}
