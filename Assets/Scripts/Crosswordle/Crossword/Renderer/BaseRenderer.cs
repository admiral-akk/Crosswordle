using UnityEngine;

public abstract class BaseRenderer : MonoBehaviour
{
    private void Start()
    {
        PaletteManager.RegisterRenderer(this);
        StartRenderer();
    }

    protected virtual void StartRenderer() { }

    private void OnDestroy()
    {
        PaletteManager.DeregisterRenderer(this);
    }
    public abstract void UpdatePalette(ColorPalette palette);
}
