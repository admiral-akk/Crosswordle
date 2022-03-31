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

    public void UpdatePosition(Vector2Int position, Vector2Int dimensions)
    {
        transform.position = new Vector3(position.x, position.y);
    }

    public void UpdateLetter(string c)
    {
        Letter.text = c;
    }
}
