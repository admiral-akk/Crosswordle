using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterSquareRenderer : CrosswordleRenderer
{
    [Header("Square Renderer")]
    [SerializeField] private Image Border;
    [SerializeField] private Image Background;
    [SerializeField] private TextMeshProUGUI Text;

    private ColorPalette _palette;
   
    private ColorPalette Palette {
        get => _palette;
        set
        {
            _palette = value;
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        Border.color = Palette.Border;
        switch (S)
        {
            case State.None:
                UpdateTextColor(Palette.None);
                break;
            case State.Wrong:
                UpdateTextColor(Palette.Wrong);
                break;
            case State.NotInCrossword:
                UpdateTextColor(Palette.NothingFound);
                break;
            case State.BadPosition:
                UpdateTextColor(Palette.BadPosition);
                break;
            case State.Correct:
                UpdateTextColor(Palette.Correct);
                break;
            case State.Empty:
                Background.color = Palette.Empty;
                break;
        }
    }

    private void UpdateTextColor(BoxColor boxColor)
    {
        Background.color = boxColor.Background;
        Text.color = boxColor.Text;
    }

    public enum State
    {
        None,
        Wrong,
        NotInCrossword,
        BadPosition,
        Correct, 
        Empty
    }

    private State _s;

    public void Render()
    {
        UpdateColor();
    }
    public void Render(State s)
    {
        S = s;
    }

    public void Render(char c, State s)
    {
        Text.text = c.ToString();
        S = s;
    }

    private State S
    {
        get => _s;
        set
        {
            _s = value;
            UpdateColor();
        }
    }

    public override void UpdatePalette(ColorPalette palette)
    {
        Palette = palette;
    }
}
