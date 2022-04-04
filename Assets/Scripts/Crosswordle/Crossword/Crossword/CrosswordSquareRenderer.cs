using System.Linq;
using TMPro;
using UnityEngine;

public class CrosswordSquareRenderer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Color None;
    [SerializeField] private Color Wrong;
    [SerializeField] private Color WrongPosition;
    [SerializeField] private Color Correct;
    [Header("Components")]
    [SerializeField] private SpriteRenderer Background;
    [SerializeField] private TextMeshProUGUI Letter;

    private static float Size(Vector2Int dimensions, Bounds bounds)
    {
        var size = Mathf.Min(bounds.size.x / dimensions.x, bounds.size.y / dimensions.y);
        return Mathf.Min(0.75f, size);
    }
    public void UpdatePosition(Vector2Int position, Vector2Int dimensions, Bounds bounds)
    {
        var size = Size(dimensions, bounds);
        transform.localScale = size * Vector3.one;
        transform.localPosition = new Vector3(position.x - dimensions.x / 2f, dimensions.y / 2f - position.y) * size;
    }

    private enum GuessState
    {
        None,
        WrongPosition,
        Correct
    }
    private GuessState _state;

    private GuessState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            switch (_state)
            {
                case GuessState.None:
                    Background.color = None;
                    break;
                case GuessState.WrongPosition:
                    Background.color = WrongPosition;
                    break;
                case GuessState.Correct:
                    Background.color = Correct;
                    break;
            }
        }
    }

    private void Awake()
    {
        State = GuessState.None;
    }

    public void UpdateState(LetterKnowledgeState knowledge)
    {
        if (knowledge.IsSolved)
        {
            Letter.text = knowledge.Answer.ToString();
            State = GuessState.Correct;
        }
        if (State == GuessState.Correct) { 
            return;
        } 
        if (knowledge.Hints.Count > 0)
        {
            Letter.text = knowledge.Hints.Aggregate("", (s, c) => s + c.ToString());
            State = GuessState.WrongPosition;
        } else
        {
            Letter.text = "";
            State = GuessState.None;
        }
    }
}
