using System.Linq;
using TMPro;
using UnityEngine;

public class HintSquareRenderer : CrosswordleRenderer
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI Letter;
    [SerializeField] private SpriteRenderer Background;
    [SerializeField] private SpriteRenderer Border;


    public void UpdateLetter(char c)
    {
        Letter.text = c.ToString();
    }

    public override void UpdatePalette(ColorPalette palette)
    {
        Letter.color = palette.BadPosition.Text;
        Background.color = palette.BadPosition.Background;
        Border.color = palette.Border;
    }

    public void UpdatePosition(int index)
    {
        transform.localScale =  Vector3.one * 0.525f;
        transform.localPosition = Vector3.right * ((index%2)*0.475f-0.5f) + Vector3.down * ((index/2) * 0.475f - 0.5f);
    }
}
