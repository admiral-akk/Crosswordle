using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardSquare : CrosswordleRenderer
{
    [SerializeField] private TextMeshProUGUI letter;
    [SerializeField] private Image background;
    [SerializeField] private Image border;

    private Color _default;
     private Color _wrong;
     private Color _wrongPosition;
    private Color _rightPosition;

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

    public State _s;
    public State S
    {
        get => _s;
        set
        {

            if (_s != State.RightPosition)
                _s = value;
            switch (_s)
            {
                case State.None:
                    background.color = _default;
                    break;
                case State.Wrong:
                    background.color = _wrong;
                    break;
                case State.WrongPosition:
                    background.color = _wrongPosition;
                    break;
                case State.RightPosition:
                    background.color = _rightPosition;
                    break;
            }
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

    public override void UpdatePalette(ColorPalette palette)
    {
        _default = palette.None.Background;
        _wrong = palette.NothingFound.Background;
        _wrongPosition = palette.BadPosition.Background;
        _rightPosition = palette.Correct.Background;
        border.color = palette.Border;
        S = S;
    }
}
