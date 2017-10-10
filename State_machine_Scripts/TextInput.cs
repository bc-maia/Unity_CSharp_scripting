using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    public InputField inputField;

    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();

        // pressing return or clicking into another UI element
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    // this function is called whenever the listener receives a onEndEdit confirmation
    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput); // prepare input to be logged in the screen,
                                                   // even if it's not runnable
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        // loop though all input and compare w/ the first word of the saparated input array
        foreach (InputAction inputAction in controller.inputActions)
        {
            // checking the first word means finding if the action exists
            if (inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }
        InputComplete();
    }

    // print out the user input to the screen, clean the input field UI element
    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}