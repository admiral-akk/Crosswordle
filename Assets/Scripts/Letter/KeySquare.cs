using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Structs.GuessResult;

public class KeySquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;
    [SerializeField] private Image background;

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

    private State _s;
    private State S
    {
        set
        {
            switch (value)
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
            _s = value;
        }
    }

    private void Awake()
    {
        S = State.None;
    }

    public void HandleResult(ResultType resultType)
    {
        switch (resultType)
        {
            case ResultType.NotInAnswer:
                S = State.Wrong;
                break;
            case ResultType.WrongPosition:
                S = State.WrongPosition;
                break;
            case ResultType.Correct:
                S = State.RightPosition;
                break;
        }
    }
}
