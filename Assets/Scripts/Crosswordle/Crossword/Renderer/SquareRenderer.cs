using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SquareRenderer : CrosswordleRenderer
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
        }
    }

    private void UpdateTextColor(BoxColor boxColor)
    {
        Background.color = boxColor.Background;
        Text.color = boxColor.Text;
    }

    protected enum State
    {
        None,
        Wrong,
        NotInCrossword,
        BadPosition,
        Correct
    }

    private State _s;

    protected void Render(string text, State s)
    {
        Text.text = text;
        S = s;
    }
    
    protected override void StartRenderer()
    {
        S = State.None;
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
        _palette = palette;
        UpdateColor();
    }
}
