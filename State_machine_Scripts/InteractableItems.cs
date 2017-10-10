using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();
    private List<string> nounsInInventory = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    // this method receives each interactable object contained in that room
    //  and compares if it is already inside the inventory list content,
    //  if its not, then add the item to the inventory list and next time not return it to the GameController
    public string GetObjectsNotInInventory(InteractableObject interactableInRoom)
    {
        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }
        return null;
    }

    public Dictionary<string, string> Take (string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];

        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There isn't " + noun + " here to take.");
            return null;
        }
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");

        if (nounsInInventory.Count != 0)
            foreach (string itemNoun in nounsInInventory)
            {
                controller.LogStringWithReturn(itemNoun);
            }
        else
            controller.LogStringWithReturn("...nothing...");
    }

    public void AddActionResponsesToUseDictionary()
    {
        foreach (string noun in nounsInInventory)
        {
            InteractableObject usableObjectInInventory = GetInteractableObjectFromUsableList(noun);

            foreach (Interaction interaction in usableObjectInInventory.interactions)
            {
                // skip this object if its interactions doesn't contains any action response
                if (interaction.actionResponse == null) 
                    continue;

                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        foreach (InteractableObject usableObj in usableItemList)
        {
            if (usableObj.noun == noun)
                return usableObj;
        }
        return null;
    }

    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                    controller.LogStringWithReturn("Nothing Happens.");
            }
            else
                controller.LogStringWithReturn("You can't use the " + nounToUse);
        }
        else
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use.");
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }
}