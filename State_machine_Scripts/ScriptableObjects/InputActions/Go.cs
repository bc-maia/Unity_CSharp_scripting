using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]
public class Go : InputAction {

    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        // the second word [1] is the noun which will drive the room choice
        controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
    }
}