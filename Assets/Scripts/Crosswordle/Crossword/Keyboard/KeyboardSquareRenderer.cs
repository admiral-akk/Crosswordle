
using UnityEngine;

public class KeyboardSquareRenderer : MonoBehaviour
{
    [SerializeField] private LetterSquareRenderer LetterSquare;
    public void UpdateColor(CharacterKnowledge.Knowledge k)
    {
        switch (k)
        {
            case CharacterKnowledge.Knowledge.None:
                LetterSquare.Render(LetterSquareRenderer.State.None);
                break;
            case CharacterKnowledge.Knowledge.NotInCrossword:
                LetterSquare.Render(LetterSquareRenderer.State.NotInCrossword);
                break;
            case CharacterKnowledge.Knowledge.Incomplete:
                LetterSquare.Render(LetterSquareRenderer.State.BadPosition);
                break;
            case CharacterKnowledge.Knowledge.Complete:
                LetterSquare.Render(LetterSquareRenderer.State.Correct);
                break;
        }
    }

    public void Initialize(char c)
    {
        LetterSquare.Render(c, LetterSquareRenderer.State.None);
    }
}
