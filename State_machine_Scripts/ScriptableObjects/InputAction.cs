using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{   
    public string keyWord; // remember to lower case the keyword, this avoids string comparison problems

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);
}
