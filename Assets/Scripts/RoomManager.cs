using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    private Room current;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //set to whatever the first room is
        current = RoomList.roomList[1];
    }

    public void LoadRoom(int exitDirection)
    {
        if (!current.hasNeighbor(exitDirection))
        {
            CreateRoom(exitDirection);
        }

        current = current.getNeighbor(exitDirection);
        SceneManager.LoadScene(current.getBuildIndex());
    }

    private void CreateRoom(int exitDirection)
    {
        //find enter direction of the new room
        int enterDirection = exitDirection - 4;
        if (enterDirection <= 0)
            enterDirection += 4;

        List<Room> validList = RoomList.getRoomsWithDoor(enterDirection);
        Room randomRoom = validList[Random.Range(0, validList.Count)];
        current.setNeighbor(exitDirection, randomRoom);
        randomRoom.setNeighbor(enterDirection, current);

        current = randomRoom;
    }
}
