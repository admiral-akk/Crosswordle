using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Assets.Scripts.Structs.GuessResult;

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
            if (GetComponent<Pop>() == null)
                gameObject.AddComponent(typeof(Pop));
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


    private IEnumerator UpdateState(State state, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.AddComponent(typeof(Pop));
        yield return new WaitForSeconds(0.1f);
        S = state;
    }

    public void HandleResult(ResultType resultType, float delay)
    {
        State newState;
        switch (resultType)
        {
            default:
                newState = State.None;
                break;
            case ResultType.NotInAnswer:
                newState = State.Wrong;
                break;
            case ResultType.WrongPosition:
                newState = State.WrongPosition;
                break;
            case ResultType.Correct:
                newState = State.RightPosition;
                break;
        }
        StartCoroutine(UpdateState(newState, delay));
    }
}
