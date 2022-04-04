using TMPro;
using UnityEngine;

public class GuessSquareRenderer : MonoBehaviour
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

    public void UpdateLetter(string c, CharacterKnowledge.Knowledge knowledge = CharacterKnowledge.Knowledge.None)
    {
        Letter.text = c;
        switch (knowledge)
        {
            case CharacterKnowledge.Knowledge.None:
                Background.color = None;
                break;
            case CharacterKnowledge.Knowledge.NotInCrossword:
                Background.color = Wrong;
                break;
            case CharacterKnowledge.Knowledge.Incomplete:
                Background.color = WrongPosition;
                break;
            case CharacterKnowledge.Knowledge.Complete:
                Background.color = Correct;
                break;
        }
    }
}
