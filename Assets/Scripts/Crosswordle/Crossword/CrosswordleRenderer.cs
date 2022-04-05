using UnityEngine;

public abstract class CrosswordleRenderer : MonoBehaviour
{
    private void Start()
    {
        PaletteManager.RegisterRenderer(this);
    }

    private void OnDestroy()
    {
        PaletteManager.DeregisterRenderer(this);
    }
    public abstract void UpdatePalette(ColorPalette palette);
}
