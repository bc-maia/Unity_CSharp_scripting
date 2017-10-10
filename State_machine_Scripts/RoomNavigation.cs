using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

    public Room currentRoom;

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    // unpacks current room data (exits and descriptions)
    public void UnpackExitsInRoom()
    {
        foreach (Exit exit in currentRoom.exits)
        {
            exitDictionary.Add(exit.keyString, exit.valueRoom);
            controller.interactionDescriptionsInRoom.Add(exit.exitDescription);
        }
    }

    // change room if the keyword matches with the specification.
    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitDictionary.ContainsKey(directionNoun))
        {   // changing current active room
            currentRoom = exitDictionary[directionNoun];
            controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
        }
        else
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}