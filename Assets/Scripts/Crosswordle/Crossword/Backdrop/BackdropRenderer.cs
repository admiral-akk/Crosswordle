using UnityEngine;

public class BackdropRenderer : CrosswordleRenderer
{
    [SerializeField] private SpriteRenderer sprite;

    public override void UpdatePalette(ColorPalette palette)
    {
        sprite.color = palette.Backdrop;
    }
}
