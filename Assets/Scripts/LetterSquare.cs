using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;
    [SerializeField] private SpriteRenderer background;

    [SerializeField] private Color Default;
    [SerializeField] private Color Wrong;
    [SerializeField] private Color WrongPosition;
    [SerializeField] private Color RightPosition;

    public char Letter
    {
        get
        {
            if (letter.text == string.Empty)
            {
                return ' ';
            } else
            {
                return letter.text[0];
            }
        }
            set
        {
            letter.text = value.ToString().ToUpper();
        }
    }

    public void ClearLetter()
    {
        letter.text = string.Empty;
    }

    public enum State
    {
        None,
        Wrong,
        WrongPosition,
        RightPosition,
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.None:
                background.color = Default;
                break;
            case State.Wrong:
                background.color = Wrong;
                break;
            case State.WrongPosition:
                background.color = WrongPosition;
                break;
            case State.RightPosition:
                background.color = RightPosition;
                break;
        }
    }
}
