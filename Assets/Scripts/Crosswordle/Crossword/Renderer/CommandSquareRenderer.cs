using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandSquareRenderer : BaseRenderer
{
    [Header("Square Renderer")]
    [SerializeField] private Image Border;
    [SerializeField] private Image Background;
    [SerializeField] private TextMeshProUGUI Text;

    private void Awake()
    {
        UpdateColor();
    }

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
        Background.color = Palette.None.Background;
        Text.color = Palette.None.Text;
    }

    public override void UpdatePalette(ColorPalette palette)
    {
        Palette = palette;
    }

    public void SetCommand(string command)
    {
        Text.text = command.ToUpper();
    }
}
