using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text displayText; // UI text

    public InputAction[] inputActions;

    // room tracker
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();

    // Items tracker
    [HideInInspector] public InteractableItems interactableItems;

    // list of room texts
    List<string> actionLog = new List<string>();

    // Use this for initialization
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
    }

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    // add string to the output log to be printed
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // print all logged text into the UI text element
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    // Unpack current room exits and data to be currently used
    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        PreparedObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    // Check if objects in room are not in inventory already
    void PreparedObjectsToTakeOrExamine(Room current)
    {
        foreach (InteractableObject interactableInRoom in current.interactableObjectsInRoom)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(interactableInRoom);

            if (descriptionNotInInventory != null)
                interactionDescriptionsInRoom.Add(descriptionNotInInventory);

            // each object is passive of some specific interaction types
            foreach (Interaction interaction in interactableInRoom.interactions)
            {
                // so if the obj has that interaction type, add its noun and response
                // for safety we can lower case the keyword if it was mispelled w higher case letter
                if (interaction.inputAction.keyWord == "examine")
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);

                if (interaction.inputAction.keyWord == "take")
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
            }
        }
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string,string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
            return verbDictionary[noun];

        return "You can't " + verb + " " + noun;
    }

    // clearing data from last room before output current room texts
    void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }
}
