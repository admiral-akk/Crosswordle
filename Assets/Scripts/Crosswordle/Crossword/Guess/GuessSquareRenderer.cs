using TMPro;
using UnityEngine;

public class GuessSquareRenderer : CrosswordleRenderer
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer Border;
    [SerializeField] private SpriteRenderer Background;
    [SerializeField] private TextMeshProUGUI Letter;

    private Color _none;
    private Color _wrong;
    private Color _wrongPosition;
    private Color _correct;
    private static float Size(Vector2Int dimensions, Bounds bounds)
    {
        var size = Mathf.Min(bounds.size.x / dimensions.x, bounds.size.y / dimensions.y);
        return Mathf.Min(1f, size);
    }
    public void UpdatePosition(Vector2Int position, Vector2Int dimensions, Bounds bounds)
    {
        var size = Size(dimensions, bounds);
        transform.localScale = size * Vector3.one;
        transform.localPosition = new Vector3(position.x - dimensions.x / 2f, -position.y + dimensions.y / 2f) * size;
    }

    private enum GuessState
    {
        None,
        NotInCrossword,
        WrongPosition,
        Correct,
    }

    private GuessState _state;

    private GuessState State
    {
        get => _state;
        set
        {
            _state = value;
            switch (_state)
            {
                case GuessState.None:
                    Background.color = _none;
                    break;
                case GuessState.NotInCrossword:
                    Background.color = _wrong;
                    break;
                case GuessState.WrongPosition:
                    Background.color = _wrongPosition;
                    break;
                case GuessState.Correct:
                    Background.color = _correct;
                    break;
            }
        }
    }

    public void UpdateLetter(string c, CharacterKnowledge.Knowledge knowledge = CharacterKnowledge.Knowledge.None)
    {
        Letter.text = c;
        switch (knowledge)
        {
            case CharacterKnowledge.Knowledge.None:
                State = GuessState.None;
                break;
            case CharacterKnowledge.Knowledge.NotInCrossword:
                State = GuessState.NotInCrossword;
                break;
            case CharacterKnowledge.Knowledge.Incomplete:
                State = GuessState.WrongPosition;
                break;
            case CharacterKnowledge.Knowledge.Complete:
                State = GuessState.Correct;
                break;
        }
    }

    public override void UpdatePalette(ColorPalette palette)
    {
        Border.color = palette.Border;
        _none = palette.None.Background;
        _wrong = palette.NothingFound.Background;
        _wrongPosition = palette.BadPosition.Background;
        _correct = palette.Correct.Background;
        State = State;
    }
}
