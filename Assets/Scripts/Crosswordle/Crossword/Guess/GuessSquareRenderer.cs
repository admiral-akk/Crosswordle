using TMPro;
using UnityEngine;

public class GuessSquareRenderer : MonoBehaviour
{
    [SerializeField] private LetterSquareRenderer LetterSquare;
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
    public void UpdateLetter(char c, GuessKnowledge.Knowledge knowledge = GuessKnowledge.Knowledge.Unused)
    {
        switch (knowledge)
        {
            case GuessKnowledge.Knowledge.Unused:
                LetterSquare.Render(c, LetterSquareRenderer.State.None);
                break;
            case GuessKnowledge.Knowledge.NotInCrossword:
                LetterSquare.Render(c, LetterSquareRenderer.State.NotInCrossword);
                break;
            case GuessKnowledge.Knowledge.CouldBeHere:
                LetterSquare.Render(c, LetterSquareRenderer.State.BadPosition);
                break;
            case GuessKnowledge.Knowledge.NotHere:
                LetterSquare.Render(c, LetterSquareRenderer.State.Wrong);
                break;
            case GuessKnowledge.Knowledge.Complete:
                LetterSquare.Render(c, LetterSquareRenderer.State.Correct);
                break;
        }
    }

}
