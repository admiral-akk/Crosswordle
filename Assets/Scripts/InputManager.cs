using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : Manager<InputManager>
{
    void OnGUI()
    {
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
            return;
            var c = (char)e.keyCode;
            if (char.IsLetter(c)) { 
                GameManager.LetterEntered(c);
                return;
            }
            if (e.keyCode == KeyCode.Return)
            {
                GameManager.WordSubmitted();
                return;
            }
            if (e.keyCode == KeyCode.Backspace)
            {
                GameManager.DeleteLetter();
                return;
            }
    }
}
