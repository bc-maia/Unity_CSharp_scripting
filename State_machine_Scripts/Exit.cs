using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit {

    public string keyString;            // keyword-trigger to exit to the room
    public string exitDescription;      // exit text description
    public Room valueRoom;              // exit destination room name
}