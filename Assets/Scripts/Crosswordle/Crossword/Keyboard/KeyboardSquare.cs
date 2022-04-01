using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardSquare : MonoBehaviour
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
            }
            else
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
        get => _s;
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
        NewGame();
    }

    public void NewGame()
    {
        S = State.None;
    }
}
