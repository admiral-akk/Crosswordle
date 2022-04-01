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
        return Mathf.Min(1f, size);
    }
    public void UpdatePosition(Vector2Int position, Vector2Int dimensions, Bounds bounds)
    {
        var size = Size(dimensions, bounds);
        transform.localScale = size * Vector3.one;
        transform.localPosition = new Vector3(position.x - dimensions.x / 2f, -position.y + dimensions.y / 2f) * size;
    }
    public void UpdateState(LetterKnowledge knowledge)
    {
        Letter.text = knowledge.PossibleLetters;
        switch (knowledge.State)
        {
            case LetterKnowledge.KnowledgeState.None:
                Background.color = None;
                break;
            case LetterKnowledge.KnowledgeState.WrongPosition:
                Background.color = WrongPosition;
                break;
            case LetterKnowledge.KnowledgeState.Correct:
                Background.color = Correct;
                break;
        }
    }
}
