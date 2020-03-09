using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList
{
    public static List<Room> roomList;

    public static void init()
    {
        roomList = new List<Room>();
    }

    public static List<Room> getRoomsWithDoor(int direction)
    {
        List<Room> subList = new List<Room>();

        foreach(Room r in roomList)
            if(r.hasDoor(direction))
                subList.Add(r);

        return subList;
    }
}
