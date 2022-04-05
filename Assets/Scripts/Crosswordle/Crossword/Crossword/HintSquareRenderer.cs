using UnityEngine;

public class HintSquareRenderer : MonoBehaviour
{
    [SerializeField] private LetterSquareRenderer LetterSquare;
    public void UpdateLetter(char c)
    {
        LetterSquare.Render(c, LetterSquareRenderer.State.BadPosition);
    }
    public void UpdatePosition(int index)
    {
        transform.localScale =  Vector3.one * 0.525f;
        transform.localPosition = Vector3.right * ((index%2)*0.475f-0.5f) + Vector3.down * ((index/2) * 0.475f - 0.5f);
    }
}
